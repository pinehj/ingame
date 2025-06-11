using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField] private List<UI_AchievementSlot> _slots;
    [SerializeField] private UI_AchievementSlot _slotPrefab;
    [SerializeField] private Transform _slotContainer;
    private void Start()
    {
        AchievementManager.Instance.OnDataChanged += Refresh;
        _slots = _slotContainer.GetComponentsInChildren<UI_AchievementSlot>().ToList<UI_AchievementSlot>();
        Refresh();
    }
    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        int achievementIndex = 0;
        int slotIndex = 0;
        for(; achievementIndex<achievements.Count; achievementIndex++)
        {
            if (achievements[achievementIndex].RewardClaimed || !achievements[achievementIndex].IsUnlocked)
            {
                continue;
            }
            if(slotIndex >= _slots.Count)
            {
                _slots.Add(Instantiate(_slotPrefab, _slotContainer));
            }
            _slots[slotIndex].Refresh(achievements[achievementIndex]);
            slotIndex++;
        }
        for(int removeIndex = _slots.Count - 1;removeIndex>=slotIndex;--removeIndex)
        {
            Destroy(_slots[removeIndex].gameObject);
            _slots.Remove(_slots[removeIndex]);
        }
    }
}