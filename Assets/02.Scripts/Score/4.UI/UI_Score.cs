using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _name;

    [SerializeField] private UI_ScoreSlot _myScoreSlot;
    [SerializeField] private Transform _rankingContainer;
    [SerializeField] private UI_ScoreSlot _scoreSlotPrefab;
    private List<UI_ScoreSlot> _slots;

    private void Start()
    {
        ScoreManager.Instance.OnDataChanged += Refresh;
        Refresh();
    }

    public void Refresh()
    {
        _slots = _rankingContainer.GetComponentsInChildren<UI_ScoreSlot>().ToList();
        _currentScore.text = $"Score\n{ScoreManager.Instance.CurrentScore.Scores.ToString("N0")}";
        _name.text = $"Name\n{ScoreManager.Instance.CurrentScore.Nickname}";

        List<ScoreDTO> scoreDTOs = ScoreManager.Instance.ScoreList;

        for (int i = 0; i < scoreDTOs.Count; ++i)
        {
            int ranking = i + 1;

            if (i >= _slots.Count)
            {
                UI_ScoreSlot newScoreSlot = Instantiate(_scoreSlotPrefab, _rankingContainer);
                newScoreSlot.Refresh(scoreDTOs[i], ranking);
            }
            else
            {
                _slots[i].Refresh(scoreDTOs[i], ranking);
            }
            if (scoreDTOs[i].Nickname == AccountManager.Instance.Nickname)
            {
                _myScoreSlot.Refresh(scoreDTOs[i], ranking);
            }
        }
    }
}