using TMPro;
using UnityEngine;

public class UI_ScoreSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ranking;
    [SerializeField] private TextMeshProUGUI _nickname;
    [SerializeField] private TextMeshProUGUI _score;

    public void Refresh(ScoreDTO scoreDTO, int ranking)
    {
        _ranking.text = ranking.ToString();
        _nickname.text = scoreDTO.Nickname;
        _score.text = scoreDTO.Scores.ToString();
    }
}
