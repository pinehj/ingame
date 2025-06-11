using System;
using UnityEngine;
[CreateAssetMenu(fileName = "AttendanceSO", menuName ="Scriptable Objects/AttendanceSO")]
public class AttendanceSO : ScriptableObject
{
    public int RewardDay;
    public ECurrencyType RewardType;
    public int RewardAmount;
}
