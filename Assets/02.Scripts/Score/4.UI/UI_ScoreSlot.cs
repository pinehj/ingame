using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScoreSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ranking;
    [SerializeField] private TextMeshProUGUI _nickname;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Image[] _rankImage;

    public void Refresh(ScoreDTO scoreDTO, int ranking)
    {
        _ranking.text = ranking.ToString("N0");
        _nickname.text = scoreDTO.Nickname;
        _score.text = scoreDTO.Scores.ToString("N0");

        for(int i = 0; i<3; i++)
        {
            if (ranking == i+1)
            {
                _rankImage[i].gameObject.SetActive(true);
            }
            else
            {
                _rankImage[i].gameObject.SetActive(false);
            }
        }
    }
}
