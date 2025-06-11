using UnityEngine;

public class AccountDTO
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public AccountDTO(string email, string nickname, string password)
    {
        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDTO(Account account)
    {
        Email = account.Email;
        Nickname = account.Nickname;
        Password = account.Password;
    }
}
