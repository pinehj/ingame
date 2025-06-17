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

        // ���̺� �����ͷ� scorelist�� �޾ƿ�
        List<Score> newList = _repository.Load();
        if(newList == null)
        {
            _scoreList.Add(_currentScore);
        }

        // ���̺� �����Ͱ� ���� ��� ���� ������ ����Ʈ�� �߰�

        // ���� �г����� ������ score���� ���� score�� Ŀ���� ��� ����
    }

    private Score FindByNickname(string nickname)
    {
        return _scoreList.Find(score => score.Nickname == nickname);
    }
}
