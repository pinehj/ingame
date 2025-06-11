using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyText;
    private void Start()
    {
        Refresh();

        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        var gold = CurrencyManager.Instance.Get(ECurrencyType.Gold);
        var diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);

        GoldCountText.text = $"Gold: {gold.Value}";
        DiamondCountText.text = $"Dimaond: {diamond.Value}";

        BuyText.color = gold.HasEnough(300) ? Color.green : Color.red;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuyHealth();
        }
    }
    public void BuyHealth()
    {
        if(CurrencyManager.Instance.Subtract(ECurrencyType.Gold, 300))
        {
            var player = FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);
        }

    }
}
