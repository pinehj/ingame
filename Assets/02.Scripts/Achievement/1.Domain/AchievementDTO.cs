using System;
using UnityEngine;

public class AchievementDTO
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public readonly int GoalValue;
    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardAmount;
    public readonly int CurrentValue;
    public readonly bool RewardClaimed;
    public readonly string ClaimedDateTime;
    public readonly bool IsUnlocked;

    public AchievementDTO(Achievement achievement)
    {
        ID = achievement.ID;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        GoalValue = achievement.GoalValue;
        RewardCurrencyType = achievement.RewardCurrencyType;
        RewardAmount = achievement.RewardAmount;
        CurrentValue = achievement.CurrentValue;
        RewardClaimed = achievement.RewardClaimed;
        ClaimedDateTime = achievement.ClaimedDateTime;
        IsUnlocked = achievement.IsUnlocked;
    }

    public bool CanClaimReward()
    {
        return (RewardClaimed == false && CurrentValue >= GoalValue);
    }
}