using UnityEngine;

public class AttendanceDTO
{
    public readonly int RewardDay;
    public readonly ECurrencyType RewardType;
    public readonly int RewardAmount;
    public readonly bool IsClaimed;

    public AttendanceDTO(Attendance attendance)
    {
        RewardDay = attendance.RewardDay;
        RewardType = attendance.RewardType;
        RewardAmount = attendance.RewardAmount;
        IsClaimed = attendance.IsClaimed;
    }


    public bool CanClaim(int currentAttendance)
    {
        return !IsClaimed && RewardDay <= currentAttendance;
    }
}
