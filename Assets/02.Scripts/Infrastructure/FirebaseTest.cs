using Firebase.Extensions;
using Firebase;
using UnityEngine;
using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;
using System;
using System.Net.Sockets;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;
    private void Start()
    {
        Init();
    }

    // ���̾�̽� �� ������Ʈ�� ����
    private void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("���̾�̽� ���ῡ �����߽��ϴ�.");
                _app = Firebase.FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;

                Login();
                AddRanking();
                GetRankings();
            }
            else
            {
                Debug.LogError($"���̾�̽� ���� �����߽��ϴ�. ${dependencyStatus}");
            }
        });
    }

    private void Register()
    {
        string email = "teemo@gmail.com";
        string password = "123456";

        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"ȸ�����Կ� �����߽��ϴ�: ${task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("ȸ�����Կ� �����߽��ϴ�: {0} ({1})", result.User.DisplayName, result.User.UserId);
            return;
        });
    }

    private void Login()
    {
        string email = "teemo@gmail.com";
        string password = "123456";

        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"�α��ο� �����߽��ϴ�: {task.Exception.Message}");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("�α��ȿ� �����߽��ϴ�: {0} ({1})", result.User.DisplayName, result.User.UserId);
        });
    }

    private void NicknameChange()
    {
        Firebase.Auth.FirebaseUser user = _auth.CurrentUser;
        if (user == null)
        {
            return;
        }

        var profile = new UserProfile
        {
            DisplayName = "teemoo"
        };
        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"�г��� ���濡 �����߽��ϴ�: {task.Exception.Message}");
                return;
            }

            Debug.Log("�г��� ���濡 �����߽��ϴ�.");
        });

    }

    private void GetProfile()
    {
        Firebase.Auth.FirebaseUser user = _auth.CurrentUser;
        if (user == null)
        {
            return;
        }

        string nickname = user.DisplayName;
        string email = user.Email;

        Account account = new Account(email, nickname, "firebase");
    }

    private void AddRanking()
    {
        Score score = new Score(80, "�׽�Ʈ");

        Dictionary<string, object> ranking = new Dictionary<string, object>
{
    { "Nickname", score.Nickname },
    { "Score", score.Scores }
};
        _db.Collection("rankings").Document(score.Nickname).SetAsync(ranking).ContinueWithOnMainThread(task => {
            Debug.Log(String.Format("Added document with ID: {0}.", task.Id));
        });
    }

    private void GetRanking()
    {
        string nickname = "�׽�Ʈ";
        DocumentReference docRef = _db.Collection("rankings").Document(nickname);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> ranking = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in ranking)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

    private void GetRankings()
    {
        // ������ �÷������κ��� �����͸� �����ö� ��� �����Ͷ��� ���� ��ɹ�
        Query allRankingsQuery = _db.Collection("rankings").OrderByDescending("Score");
        allRankingsQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allRankingsQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allRankingsQuerySnapshot.Documents)
            {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> ranking = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in ranking)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }

                // Newline to separate entries
                Debug.Log("");
            }
        });
    }
}
