using JetBrains.Annotations;
using UnityEngine;

public class AccountRepository : MonoBehaviour
{
    public const string SAVE_PREFIX = "ACCOUNT_";

    public void Save(AccountDTO accountDTO)
    {
        AccountSaveData data = new AccountSaveData(accountDTO);
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SAVE_PREFIX + data.Email, json);
    }

    public AccountSaveData Find(string email)
    {
        if(!PlayerPrefs.HasKey(SAVE_PREFIX + email))
        {
            return null;
        }

        return JsonUtility.FromJson<AccountSaveData>(PlayerPrefs.GetString(SAVE_PREFIX + email));
    }
}
