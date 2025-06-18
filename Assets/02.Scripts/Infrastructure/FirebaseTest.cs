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

    // 파이어베이스 내 프로젝트에 연결
    private void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("파이어베이스 연결에 성공했습니다.");
                _app = Firebase.FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;

                Login();
                AddRanking();
                GetRankings();
            }
            else
            {
                Debug.LogError($"파이어베이스 연결 실패했습니다. ${dependencyStatus}");
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
                Debug.LogError($"회원가입에 실패했습니다: ${task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("회원가입에 성공했습니다: {0} ({1})", result.User.DisplayName, result.User.UserId);
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
                Debug.LogError($"로그인에 실패했습니다: {task.Exception.Message}");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("로그읜에 성공했습니다: {0} ({1})", result.User.DisplayName, result.User.UserId);
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
                Debug.LogError($"닉네임 변경에 실패했습니다: {task.Exception.Message}");
                return;
            }

            Debug.Log("닉네임 변경에 성공했습니다.");
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
        Score score = new Score(80, "테스트");

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
        string nickname = "테스트";
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
        // 쿼리란 컬렉션으로부터 데이터를 가져올때 어떻게 가져와라라고 쓰는 명령문
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
