using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttendanceSlot : MonoBehaviour
{
    private AttendanceDTO _attendanceDTO;
    public CanvasGroup SlotGroup;
    public Button ClaimButton;
    public Image RewardImage;
    public TextMeshProUGUI RewardDayTextUI;
    public TextMeshProUGUI RewardAmountTextUI;

    public void Refresh(AttendanceDTO attendanceDTO)
    {
        _attendanceDTO = attendanceDTO;
        ClaimButton.onClick.AddListener(() => { ClaimReward(); });
        RewardDayTextUI.text = attendanceDTO.RewardDay.ToString("D2");
        string claimText = attendanceDTO.IsClaimed ? "Rewarded" : "";
        RewardAmountTextUI.text = $"{claimText}x{attendanceDTO.RewardAmount.ToString("D3")}";
        SlotGroup.alpha = attendanceDTO.CanClaim(AttendanceManager.Instance.CurrentAttendnace) ? 1 : 0.5f;
        ClaimButton.interactable = attendanceDTO.CanClaim(AttendanceManager.Instance.CurrentAttendnace) ? true : false;
    }

    public void ClaimReward()
    {
        if (AttendanceManager.Instance.TryClaim(_attendanceDTO))
        {
            // 성공 이펙트
        }
        else
        {
            // 진행도 부족 팝업
        }
    }
}
