using UnityEngine;
using System;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private ScoreRepository _repository;

    private List<Score> _scoreList;
    private Score _currentScore;
    public Score CurrentScore => _currentScore;
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

        // 세이브 데이터로 scorelist를 받아옴
        List<Score> newList = _repository.Load();
        if(newList == null)
        {
            _scoreList.Add(_currentScore);
        }

        // 세이브 데이터가 없을 경우 현재 점수만 리스트에 추가

        // 같은 닉네임의 데이터 score보다 현재 score가 커졌을 경우 갱신
    }

    private Score FindByNickname(string nickname)
    {
        return _scoreList.Find(score => score.Nickname == nickname);
    }
}
