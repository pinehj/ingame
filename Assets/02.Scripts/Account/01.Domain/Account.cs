using System;
using System.Text.RegularExpressions;
using static UnityEngine.Rendering.DebugUI;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public Account(string email, string nickname, string password)
    {

        // �̸��� ����
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        // �г��� ����
        var nickNameSpecification = new AccountNicknameSpecification();
        if (!nickNameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nickNameSpecification.ErrorMessage);
        }


        // ��й�ȣ ����
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("��й�ȣ�� ������� �� �����ϴ�.");
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, Nickname, Password);
    }
}