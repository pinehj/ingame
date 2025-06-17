using System;
using UnityEngine;

[Serializable]
public class ScoreDTO
{
    public int Scores;
    public string Nickname;

    public ScoreDTO() { }

    public ScoreDTO(Score score)
    {
        Scores = score.Scores;
        Nickname = score.Nickname;
    }
}
