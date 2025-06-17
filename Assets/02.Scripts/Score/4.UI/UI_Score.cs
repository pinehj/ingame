using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _name;

    [SerializeField] private UI_ScoreSlot _myScoreSlot;
    [SerializeField] private Transform _rankingContainer;
    [SerializeField] private UI_ScoreSlot _scoreSlotPrefab;

    public void Refresh()
    {
        _currentScore.text = $"Score\n{ScoreManager.Instance.CurrentScore.Scores.ToString()}";
        _name.text = $"Name\n{ScoreManager.Instance.CurrentScore.Nickname}";

        List<ScoreDTO> scoreDTOs = ScoreManager.Instance.ScoreList;

        for (int i = 0; i < scoreDTOs.Count; ++i)
        {
            int ranking = i + 1;
            UI_ScoreSlot newScoreSlot = Instantiate(_scoreSlotPrefab, _rankingContainer);
            newScoreSlot.Refresh(scoreDTOs[i], ranking);

            if (scoreDTOs[i].Nickname == AccountManager.Instance.Nickname)
            {
                _myScoreSlot.Refresh(scoreDTOs[i], i);
            }
        }
    }
}