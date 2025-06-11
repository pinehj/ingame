using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;

    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDateTextUI;
    public Button RewardClaimButton;

    private AchievementDTO _achievementDTO;

    public void Refresh(AchievementDTO achievementDTO)
    {
        _achievementDTO = achievementDTO;
        NameTextUI.text = _achievementDTO.Name;
        DescriptionTextUI.text = _achievementDTO.Description;
        RewardCountTextUI.text = _achievementDTO.RewardAmount.ToString();

        ProgressSlider.value = (float)_achievementDTO.CurrentValue / _achievementDTO.GoalValue;
        ProgressTextUI.text = $"{_achievementDTO.CurrentValue}/{_achievementDTO.GoalValue}";

        RewardClaimDateTextUI.text = _achievementDTO.ClaimedDateTime;

        RewardClaimButton.interactable = _achievementDTO.CanClaimReward();
        RewardClaimButton.onClick.AddListener(() => { ClaimReward(); });
    }

    public void ClaimReward()
    {
        if (AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
            // 성공 이펙트
        }
        else
        {
            // 진행도 부족 팝업
        }
    }
}
