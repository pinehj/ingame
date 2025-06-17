using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections.LowLevel.Unsafe;
public class StageLevel
{
    // ��ȹ ������
    public readonly string Name;
    public readonly int StartLevel;
    public readonly int EndLevel;
    public float HealthFactor;
    public float DamageFactor;
    public float Duration => 60f;
    public readonly float SpawnInterval;
    public readonly float SpawnRate;


    // ���� ������
    public int CurrentLevel { get; private set; }

    public StageLevel(StageLevelSO SO, int currentLevel) : this(SO.Name, SO.StartLevel, SO.EndLevel, SO.HealthFactor, SO.DamageFactor, SO.SpawnInterval, SO.SpawnRate, currentLevel)
    {

    }
    public StageLevel(string name, int startLevel, int endlevel, float healthFactor, float damageFactor, float spawnInterval, float spawnRate, int currentLevel)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("�ùٸ��� ���� name�Դϴ�.");
        }
        if (startLevel < 0 || endlevel < startLevel)
        {
            throw new Exception("������ �ɹٸ��� �ʽ��ϴ�.");
        }

        if (healthFactor < 1)
        {
            throw new Exception("ü�� ������ �ùٸ��� �ʽ��ϴ�.");
        }
        if (damageFactor < 1)
        {
            throw new Exception("���ݷ� ������ �ùٸ��� �ʽ��ϴ�.");
        }

        if (spawnInterval <= 0)
        {
            throw new Exception("���� �ֱⰡ �ùٸ��� �ʽ��ϴ�.");
        }
        if (spawnRate <= 0 || 100 < spawnRate)
        {
            throw new Exception("���� Ȯ���� �ùٸ��� �ʽ��ϴ�.");
        }

        if (currentLevel < startLevel || endlevel < currentLevel)
        {
            throw new Exception("���� ������ �ùٸ��� �ʽ��ϴ�.");
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
