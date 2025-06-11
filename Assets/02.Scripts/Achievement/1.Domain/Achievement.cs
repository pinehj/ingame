using System;
using UnityEngine;


public enum EAchievementCondition
{
    GoldCollect,
    DronKillCount,
    BossKillCount,
    PlayTime,
    Trigger,
    Attendance
}

[Serializable]
public class Achievement
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;


    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;

    private string _claimedDateTime;
    public string ClaimedDateTime => _claimedDateTime;

    private bool _isUnlocked;
    public bool IsUnlocked => _isUnlocked;

    public readonly string[] TraillingAchievementID;
    public Achievement(AchievementSO metaData, AchievementSaveData saveData)
    {
        if (string.IsNullOrEmpty(metaData.ID))
        {
            throw new Exception("업적 아이디은 비어있을 수 없습니다.");
        }

        if (string.IsNullOrEmpty(metaData.Name))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }

        if (string.IsNullOrEmpty(metaData.Description))
        {
            throw new Exception("업적 설명은 비어있을 수 없습니다.");
        }

        if (metaData.GoalValue <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야합니다.");
        }

        if (metaData.RewardAmount <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야합니다.");
        }

        ID = metaData.ID;
        Name = metaData.Name;
        Description = metaData.Description;
        Condition = metaData.Condition;
        GoalValue = metaData.GoalValue;
        RewardCurrencyType = metaData.RewardCurrencyType;
        RewardAmount = metaData.RewardAmount;

        _currentValue = saveData.CurrentValue;
        _rewardClaimed = saveData.RewardClaimed;
        _claimedDateTime = saveData.ClaimedDateTime;

        _isUnlocked = saveData.IsUnlocked;
        TraillingAchievementID = metaData.TraillingAchievementID;
    }

    public void Increase(int value)
    {
        if (value <= 0)
        {
            throw new Exception("증가 값은 0보다 커야합니다.");
        }
        _currentValue += value;
    }

    public bool CanClaimReward()
    {
        return (RewardClaimed == false && CurrentValue >= GoalValue);
    }

    public bool TryClaimReward()
    {
        if (!CanClaimReward())
        {
            return false;
        }

        _rewardClaimed = true;
        _claimedDateTime = DateTime.Today.ToString("yyyy.MM.dd");
        return true;
    }

    public void Unlock()
    {
        _isUnlocked = true;
    }
}
