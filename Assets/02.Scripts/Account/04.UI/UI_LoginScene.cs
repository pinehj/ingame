using System;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText;
    public TMP_InputField EmailInputField;
    public TMP_InputField NicknameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordConfirmInputField;
    public Button ConfirmButton;
}
public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]

    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    [Header("로그인")]
    public UI_InputFields LoginInputFields;
    [Header("회원가입")]
    public UI_InputFields RegisterInputFields;

    [Header("결과 알림 효과")]
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;

    private const string PREFIX = "ID_";
    private const string SALT = "19521";
    private void Start()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);

        LoginInputFields.ResultText.text = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;

        //LoginCheck();
    }

    public void OnClickGoToRegisterButton()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
        LoginInputFields.ResultText.text = string.Empty;
    }
    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        RegisterInputFields.ResultText.text = string.Empty;
    }

    public void Login()
    {
        string email = LoginInputFields.EmailInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            AlertResult(LoginInputFields, emailSpecification.ErrorMessage);
            return;
        }


        string password = LoginInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            AlertResult(LoginInputFields, passwordSpecification.ErrorMessage);
            return;
        }

        if (AccountManager.Instance.TryLogin(email, password))
        {
            SceneManager.LoadScene(1); 
        }
        else
        {
            AlertResult(LoginInputFields, "올바른 이메일 형식이 아닙니다.");
        }
    }
    public void Register()
    {
        string email = RegisterInputFields.EmailInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            AlertResult(RegisterInputFields, emailSpecification.ErrorMessage);
            return;
        }

        string nickname = RegisterInputFields.NicknameInputField.text;
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            AlertResult(RegisterInputFields, nicknameSpecification.ErrorMessage);
            return;
        }

        string password = RegisterInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            AlertResult(RegisterInputFields, passwordSpecification.ErrorMessage);
            return;
        }


        string passwordConfirm = RegisterInputFields.PasswordConfirmInputField.text;
        if (password != passwordConfirm)
        {
            RegisterInputFields.ResultText.text = "비밀번혹가 다릅니다.";
            return;
        }
        if (!passwordSpecification.IsSatisfiedBy(passwordConfirm))
        {
            AlertResult(RegisterInputFields, passwordSpecification.ErrorMessage);
            return;
        }



        if(AccountManager.Instance.TryRegister(email, nickname, password))
        {
            OnClickGoToLoginButton();
        }
    }

    private void AlertResult(UI_InputFields inputFields, string message)
    {
        inputFields.ResultText.text = message;
        //inputFields.ResultText.transform.DOShakePosition(_shakeDuration, _shakeStrength, randomness:0);
    }


    public void LoginCheck()
    {
        string id = LoginInputFields.EmailInputField.text;
        string password = LoginInputFields.PasswordInputField.text;

        LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password);
    }
}
