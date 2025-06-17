using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Stage
{
    public int LevelNumber { get; private set; }
    public int SubLevelNumber => _curentLevel.CurrentLevel;
    private StageLevel _curentLevel;
    public StageLevel CurrentLevel => _curentLevel;
    private float _progressTime;
    public List<StageLevel> Levels { get; private set; } = new List<StageLevel>();
    public Stage(int levelNumber, int subLevelNumber, float progressTime, List<StageLevelSO> levelSOList)
    {
        if(levelNumber < 0)
        {
            throw new Exception("올바르지 않은 레벨 넘버입니다.");
        }
        if(subLevelNumber < 0)
        {
            throw new Exception("올바르지 않은 서브레벨 넘버입니다.");
        }
        if(progressTime < 0)
        {
            throw new Exception("올바르지 않은 진행 시간입니다.");
        }
        if(levelSOList == null)
        {
            throw new Exception("올바르지 않은 레벨 데이터입니다.");
        }

        LevelNumber = levelNumber;
        _progressTime = progressTime;

        foreach(var levelSO in levelSOList)
        {
            int sub = levelSO.StartLevel;
            if(sub < subLevelNumber)
            {
                sub = levelSO.EndLevel;

                if(subLevelNumber < sub)
                {
                    sub = subLevelNumber;
                }
            }
            AddLevel(new StageLevel(levelSO, sub));
        }
        _curentLevel = Levels[LevelNumber - 1];
    }
    private void AddLevel(StageLevel level)
    {
        if(level == null)
        {
            throw new System.Exception("레벨이 null입니다.");
        }
        Levels.Add(level);
    }

    public void Progress(float dt, Action onDataChanged)
    {
        _progressTime += dt;

        if (_curentLevel.TryLevelUp(_progressTime))
        {
            _progressTime = 0;
            if (_curentLevel.IsClear())
            {
                LevelNumber += 1;
                _curentLevel = Levels[LevelNumber - 1];
            }
            onDataChanged?.Invoke();
        }
    }

}
