using System;

public class Score
{
    private int _scores;
    public int Scores => _scores;
    public readonly string Nickname;

    public Score(int scores, string nickname)
    {
        _scores = scores;
        Nickname = nickname;
    }

    public Score(ScoreDTO dto)
    {
        _scores = dto.Scores;
        Nickname = dto.Nickname;
    }

    public void AddScore(int score)
    {
        _scores += score;
    }
}