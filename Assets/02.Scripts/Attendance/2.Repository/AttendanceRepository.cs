using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceRepository
{
    private const string SAVE_KEY = nameof(AttendanceRepository);
    public void Save(List<AttendanceDTO> attendanceDTOs, int currentAttendance, DateTime lastAttendanceDate, string email)
    {
        AttendanceSaveDataList datas = new AttendanceSaveDataList()
        {
            AttendacneSaveDatas = attendanceDTOs.ConvertAll(attendanceDTO => new AttendanceSaveData()
            {
                RewardAmount = attendanceDTO.RewardAmount,
                RewardDay = attendanceDTO.RewardDay,
                RewardType = attendanceDTO.RewardType,
                IsClaimed = attendanceDTO.IsClaimed
            }),
            CurrentAttendance = currentAttendance,
            LastAttendanceDate = lastAttendanceDate.ToString()
        };
        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY + email, json);
    }

    public AttendanceSaveDataList Load(string email)
    {
        string json = PlayerPrefs.GetString(SAVE_KEY + email);
        return JsonUtility.FromJson<AttendanceSaveDataList>(json);
    }
}

[Serializable]
public class AttendanceSaveDataList
{
    public List<AttendanceSaveData> AttendacneSaveDatas;
    public int CurrentAttendance;
    public string LastAttendanceDate;
}
