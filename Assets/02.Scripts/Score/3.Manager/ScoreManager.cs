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
            _scoreList.Add(_currentScore);
        }
        else
        {
            _scoreList = newList.ConvertAll(score => new Score(score));
        }

        // 같은 닉네임의 데이터 score보다 현재 score가 커졌을 경우 갱신
        Score highScore = FindByNickname(_currentScore.Nickname);
        if (highScore == null)
        {
            _scoreList.Add(_currentScore);
        }
        else if (highScore.Scores < _currentScore.Scores)
        {
            _scoreList.Remove(highScore);
            _scoreList.Add(_currentScore);
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
    }
}
