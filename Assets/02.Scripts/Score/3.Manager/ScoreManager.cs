using UnityEngine;
using System;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private ScoreRepository _repository;

    private List<Score> _scoreList;
    public List<ScoreDTO> ScoreList => _scoreList.ConvertAll(score => new ScoreDTO(score));
    private Score _currentScore;
    public ScoreDTO CurrentScore => new ScoreDTO(_currentScore);

    private bool _newHighScore = false;

    public Action OnDataChanged;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    private void Init()
    {
        _scoreList = new List<Score>();
        _repository = new ScoreRepository();

        string nickname = AccountManager.Instance.Nickname;
        _currentScore = new Score(0, nickname);

        // 세이브 데이터로 scorelist를 받아옴
        List<ScoreDTO> newList = _repository.Load();
        if(newList == null)
        {
            // 세이브 데이터가 없을 경우 현재 점수만 리스트에 추가
            _scoreList = new List<Score>
            {
                new Score(50, "하얀악마"),
                new Score(40, "별이되다"),
                new Score(35, "붉은혜성"),
                new Score(25, "네오목마"),
                new Score(20, "하사웨이"),
                new Score(15, "수성마녀")
            };
            _scoreList.Add(_currentScore);
        }
        else
        {
            _scoreList = newList.ConvertAll(score => new Score(score));
        }

        // 점수 높은 순으로 정렬
        _scoreList.Sort((a, b) => b.Scores.CompareTo(a.Scores));

        // 저장
        _repository.Save(ScoreList);

        OnDataChanged?.Invoke();
    }

    private Score FindByNickname(string nickname)
    {
        return _scoreList.Find(score => score.Nickname == nickname);
    }

    public void TryAddScore(int score)
    {
        _currentScore.AddScore(score);

        Score highScore = FindByNickname(_currentScore.Nickname);
        if(_newHighScore == false)
        {
            if (highScore != null && highScore.Scores < _currentScore.Scores)
            {
                _scoreList.Remove(highScore);
                _scoreList.Add(_currentScore);
                _newHighScore = true;
            }
        }
        _scoreList.Sort((a, b) => b.Scores.CompareTo(a.Scores));
        _repository.Save(ScoreList);

        OnDataChanged?.Invoke();
    }
}
