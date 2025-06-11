using System;
using UnityEngine;

[Serializable]
public class AttendanceSaveData
{
    public int RewardDay;
    public ECurrencyType RewardType;
    public int RewardAmount;
    public bool IsClaimed;
}
