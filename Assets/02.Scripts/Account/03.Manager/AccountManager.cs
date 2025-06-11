using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;

public class AccountManager : BehaviourSingleton<AccountManager>
{
    private const string SALT = "123456";
    private Account _myAccount;
    public string Email => _myAccount.Email;
    private AccountRepository _repository;

    private void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (i != this)
        {
            Destroy(gameObject);
        }
            Init();
    }

    private void Init()
    {
        _repository = new AccountRepository();
    }
    public bool TryRegister(string email, string nickname, string password)
    {
        AccountSaveData saveData = _repository.Find(email);

        if (saveData != null)
        {
            return false;
        }


        string encryptedPassword = CryptoUtil.Encryption(password, SALT);
        Account account = new Account(email, nickname, encryptedPassword);
        _repository.Save(new AccountDTO(account));

        return true;
    }
   public bool TryLogin(string email, string password)
    {
        AccountSaveData saveData = _repository.Find(email);
        if(saveData == null)
        {
            return false;
        }

        if(CryptoUtil.Verify(password, saveData.Password, SALT))
        {
            _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
            return true;
        }

        return false;
    }



}
