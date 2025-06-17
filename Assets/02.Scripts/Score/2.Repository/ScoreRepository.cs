using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ScoreSaveData
{
    public List<ScoreDTO> Scores;

    public ScoreSaveData(List<ScoreDTO> scores)
    {
        Scores = scores;
    }
}

public class ScoreRepository
{
    private const string SAVE_KEY = nameof(ScoreRepository);
    public void Save(List<ScoreDTO> scores)
    {
        ScoreSaveData data = new ScoreSaveData(scores);
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<ScoreDTO> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        ScoreSaveData datas = JsonUtility.FromJson<ScoreSaveData>(json);
        return datas.Scores;
    }
}
