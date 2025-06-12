using System;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class AttendanceManager : BehaviourSingleton<AttendanceManager>
{
    private AttendanceRepository _repository;

    private int _currentAttendance;
    public int CurrentAttendnace => _currentAttendance;

    private DateTime _lastAttendnaceDate;
    public event Action OnDataChanged;

    [SerializeField] private List<Attendance> _attendances;
    public List<AttendanceDTO> Attendances => _attendances.ConvertAll(attendance => attendance.ToDTO());

    [SerializeField] private List<AttendanceSO> _metaDatas;
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Attend();
    }

    private void Update()
    {
    }
    private void Init()
    {
        _repository = new AttendanceRepository();
        _attendances = new List<Attendance>();
        AttendanceSaveDataList loadedDatas = _repository.Load(AccountManager.Instance.Email);

        if (loadedDatas == null)
        {
            loadedDatas = new AttendanceSaveDataList()
            {
                AttendacneSaveDatas = null,
                CurrentAttendance = 0,
                LastAttendanceDate = DateTime.MinValue.ToString()
            };
        }

        List<AttendanceSaveData> saveDatas = loadedDatas.AttendacneSaveDatas;
        _currentAttendance = loadedDatas.CurrentAttendance;
        DateTime.TryParse(loadedDatas.LastAttendanceDate, out _lastAttendnaceDate);

        foreach (AttendanceSO metaData in _metaDatas)
        {
            AttendanceSaveData saveData = loadedDatas.AttendacneSaveDatas?.Find(d => d.RewardDay == metaData.RewardDay) ?? new AttendanceSaveData();
            _attendances.Add(new Attendance(metaData, saveData));
        }
    }

    public void Attend()
    {

        //if (DateTime.Now > _lastAttendnaceDate.Date)
        {
            _currentAttendance++;
            if(_currentAttendance > 7)
            {
                foreach(Attendance attendance in _attendances)
                {
                    if (attendance.CanClaim(_currentAttendance))
                    {
                        TryClaim(attendance.ToDTO());
                        attendance.IsClaimed = false;
                    }
                }

                _currentAttendance %= 7;
                
            }
            
            
            _lastAttendnaceDate = DateTime.Today;
            OnDataChanged?.Invoke();
            _repository.Save(Attendances, CurrentAttendnace, _lastAttendnaceDate, AccountManager.Instance.Email);
        }



    }

    public bool TryClaim(AttendanceDTO attendanceDTO)
    {
        Attendance attendance = _attendances.Find(attendance => attendance.RewardDay == attendanceDTO.RewardDay);

        if (attendance == null)
        {
            return false;
        }
        if (attendance.TryClaimReward(_currentAttendance))
        {
            CurrencyManager.Instance.Add(attendance.RewardType, attendance.RewardAmount);
            OnDataChanged?.Invoke();
            _repository.Save(Attendances, CurrentAttendnace, _lastAttendnaceDate, AccountManager.Instance.Email);

            AchievementManager.Instance.Increase(EAchievementCondition.Attendance, 1);
            return true;
        }
        return false;
    }
}
