using System;

public class Score
{
    private int _scores;
    public int Scores => _scores;
    public readonly string Nickname;

    public Score(int scores, string nickname)
    {

        if(scores < 0)
        {
            throw new Exception("점수는 0보다 작을 수 없습니다.");
        }
        var nickNameSpecification = new AccountNicknameSpecification();
        if (!nickNameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nickNameSpecification.ErrorMessage);
        }


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