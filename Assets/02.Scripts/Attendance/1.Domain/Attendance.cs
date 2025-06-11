using System;
using Unity.FPS.Game;
using UnityEngine;

public class Attendance
{
    public readonly int RewardDay;
    public readonly ECurrencyType RewardType;
    public readonly int RewardAmount;
    public bool IsClaimed { get; private set; }

    public Attendance(AttendanceSO metaData, AttendanceSaveData saveData)
    {
        if(metaData.RewardDay < 0)
        {
            throw new Exception("�������� 0���� ���� �� �����ϴ�.");
        }
        if(metaData.RewardAmount < 0)
        {
            throw new Exception("������ 0���� ���� �� �����ϴ�.");
        }
        RewardDay = metaData.RewardDay;
        RewardType = metaData.RewardType;
        RewardAmount = metaData.RewardAmount;

        IsClaimed = saveData.IsClaimed;
    }
    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO(this);
    }

    public bool CanClaim(int currentAttendance)
    {
        return !IsClaimed && RewardDay <= currentAttendance;
    }


    public bool TryClaimReward(int currentAttendance)
    {
        if (!CanClaim(currentAttendance))
        {
            return false;
        }

        IsClaimed = true;
        return true;
    }
}
