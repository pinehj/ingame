using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class AchievementManager : BehaviourSingleton<AchievementManager>
{
    private AchievementRepository _repository;
    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll(achievement => new AchievementDTO(achievement));
    [SerializeField] private List<AchievementSO> _metaDatas;
    public event Action OnDataChanged;
    public event Action<AchievementDTO> OnAchievementConditionMet;

    private string[] InitAchievements;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _repository = new AchievementRepository();
        _achievements = new List<Achievement>(_metaDatas.Count);


        List<AchievementSaveData> loadedDatas = _repository.Load();

        foreach (AchievementSO metaData in _metaDatas)
        {
            Achievement duplicatedAchievement = FindByID(metaData.ID);

            if (duplicatedAchievement != null)
            {
                throw new Exception($"업적 ID{metaData.ID}가 중복됩니다.");
            }

            AchievementSaveData saveData = loadedDatas?.Find(d => d.ID == metaData.ID) ?? new AchievementSaveData();
            _achievements.Add(new Achievement(metaData, saveData));
        }

        InitAchievements = new string[]{ "ACH_MONEY_001", "ACH_KILL_DRONE_001", "ACH_KILL_BOSS_001", "ACH_TIME_001", "ACH_HIDDEN_001", "ACH_ATTEND_001"};
        foreach(string id in InitAchievements)
        {
            FindByID(id).Unlock();
        }
    }

    private Achievement FindByID(string id)
    {
        return _achievements.Find(achievement => achievement.ID == id);
    }
    public void Increase(EAchievementCondition condition, int value)
    {
        foreach(var achievemenet in _achievements)
        {
            if (achievemenet.Condition == condition)
            {
                bool prevCanClaimReward = achievemenet.CanClaimReward();
                achievemenet.Increase(value);
                OnDataChanged?.Invoke();
                _repository.Save(Achievements);

                if(prevCanClaimReward != achievemenet.CanClaimReward() && !achievemenet.RewardClaimed)
                {
                    Debug.Log($"{condition}:{achievemenet.CurrentValue}/{achievemenet.GoalValue}");
                    OnAchievementConditionMet?.Invoke(new AchievementDTO(achievemenet));
                }
            }
        }
    }

    public bool TryClaimReward(AchievementDTO achievementDTO)
    {
        Achievement achievement = _achievements.Find(achievement => achievement.ID == achievementDTO.ID);

        if(achievement == null)
        {
            return false;
        }
        if (achievement.TryClaimReward()) 
        { 
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);
            foreach(string id in achievement.TraillingAchievementID)
            {
                FindByID(id).Unlock();
            }
            OnDataChanged?.Invoke();
            _repository.Save(Achievements);
            return true;
        }
        return false;
    }
}
