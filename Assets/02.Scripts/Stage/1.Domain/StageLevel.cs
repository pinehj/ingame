using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections.LowLevel.Unsafe;
public class StageLevel
{
    // 기획 데이터
    public readonly string Name;
    public readonly int StartLevel;
    public readonly int EndLevel;
    public float HealthFactor;
    public float DamageFactor;
    public float Duration => 60f;
    public readonly float SpawnInterval;
    public readonly float SpawnRate;


    // 상태 데이터
    public int CurrentLevel { get; private set; }

    public StageLevel(StageLevelSO SO, int currentLevel) : this(SO.Name, SO.StartLevel, SO.EndLevel, SO.HealthFactor, SO.DamageFactor, SO.SpawnInterval, SO.SpawnRate, currentLevel)
    {

    }
    public StageLevel(string name, int startLevel, int endlevel, float healthFactor, float damageFactor, float spawnInterval, float spawnRate, int currentLevel)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("올바르지 않은 name입니다.");
        }
        if (startLevel < 0 || endlevel < startLevel)
        {
            throw new Exception("레벨이 옵바르지 않습니다.");
        }

        if (healthFactor < 1)
        {
            throw new Exception("체력 배율이 올바르지 않습니다.");
        }
        if (damageFactor < 1)
        {
            throw new Exception("공격력 배율이 올바르지 않습니다.");
        }

        if (spawnInterval <= 0)
        {
            throw new Exception("스폰 주기가 올바르지 않습니다.");
        }
        if (spawnRate <= 0 || 100 < spawnRate)
        {
            throw new Exception("스폰 확률이 올바르지 않습니다.");
        }

        if (currentLevel < startLevel || endlevel < currentLevel)
        {
            throw new Exception("현재 레벨이 올바르지 않습니다.");
        }
        Name = name;
        StartLevel = startLevel;
        EndLevel = endlevel;
        HealthFactor = healthFactor;
        DamageFactor = damageFactor;
        SpawnInterval = spawnInterval;
        SpawnRate = spawnRate;
        CurrentLevel = currentLevel;
    }

    public bool TryLevelUp(float progressTime)
    {
        if(CurrentLevel >= EndLevel)
        {
            return false;
        }
        if(progressTime >= Duration)
        {
            CurrentLevel += 1;
            return true;
        }
        return false;
    }

    public bool IsClear()
    {
        return CurrentLevel == EndLevel;
    }
}
