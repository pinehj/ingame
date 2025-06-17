using TMPro;
using UnityEngine;

public class UI_ScoreSlot : MonoBehaviour
{
    private TextMeshProUGUI _ranking;
    private TextMeshProUGUI _nickname;
    private TextMeshProUGUI _score;

    public void Refresh(ScoreDTO scoreDTO, int ranking)
    {
        _ranking.text = ranking.ToString();
        _nickname.text = scoreDTO.Nickname;
        _score.text = scoreDTO.Scores.ToString();
    }
}
