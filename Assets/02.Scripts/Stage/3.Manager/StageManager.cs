using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : BehaviourSingleton<StageManager>
{
    public event Action OnDataChanged;

    [SerializeField]
    private List<StageLevelSO> _levelSOList;
    private Stage _stage;
    public Stage Stage => _stage;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _stage = new Stage(1, 2, 17, _levelSOList);
        OnDataChanged?.Invoke();
    }

    private void Update()
    {
        _stage.Progress(Time.deltaTime, OnDataChanged);
    }
}
