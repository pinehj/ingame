using System;
using UnityEngine;

[Serializable]
public class AccountSaveData
{
    public string Email;
    public string Nickname;
    public string Password;

    public AccountSaveData(AccountDTO accountDTO)
    {
        Email = accountDTO.Email;
        Nickname = accountDTO.Nickname;
        Password = accountDTO.Password;
    }
}
