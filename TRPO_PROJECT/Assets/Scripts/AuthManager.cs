using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase;
using TMPro;


public class AuthManager : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public TextMeshProUGUI debugText;

    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    [HideInInspector]
    public FirebaseAuth auth;
    FirebaseUser user;
    FirebaseFirestore db;
    DocumentReference docRef;

    int coinAcquired = 0;
    int level = 1;
    int enemiesKilled = 0;
    double distanceTravelled = 0.1;

    string weaponSpeedLevel = "1";
    string characterSpeedLevel = "1";
    string healingAmountLevel = "1";

    string email;
    string password;

    

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>{
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        debugText.text = auth + "     " + dependencyStatus.ToString();
    }



    public void LoginButtonClicked()
    {
        //InitializeFirebase();

        email = emailInputField.text;
        password = passwordInputField.text;
        //Debug.Log(email);
        //Debug.Log(password);

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                debugText.text = "SignInWithEmailAndPasswordAsync was canceled.";
                return;
            }
            if (task.IsFaulted)
            {
                debugText.text = "SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception;
                return;
            }

            AuthResult result = task.Result;
            FirebaseUser newUser = result.User;
            debugText.text = "User signed in successfully";

            SceneManager.LoadScene("MainMenu");
        });
    }



    public void RegisterButtonClicked()
    {   
        //InitializeFirebase();
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        db = FirebaseFirestore.DefaultInstance;
        docRef = db.Collection("users").Document(user.UserId);

        email = emailInputField.text;
        password = passwordInputField.text;
        //Debug.Log(email);
        //Debug.Log(password);

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                debugText.text = "CreateUserWithEmailAndPasswordAsync was canceled.";
                return;
            }
            if (task.IsFaulted)
            {
                debugText.text = "CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception;
                return;
            }

            AuthResult result = task.Result;
            FirebaseUser newUser = result.User;
            debugText.text = "Firebase user created successfully";
        
            // Создайте словарь для обновления
            Dictionary<string, object> userFields = new Dictionary<string, object>
            {
                { "email", email },
                { "level", level },
                { "enemiesKilled", enemiesKilled },
                { "distanceTravelled", distanceTravelled },
                { "coinsAll", coinAcquired }
            };
            db.Collection("users").Document(user.UserId).Collection("userInfo").Document("details").SetAsync(userFields);

            Dictionary<string, string> upgrades = new Dictionary<string, string>
            {
                { "weaponSpeedLevel", weaponSpeedLevel },
                { "characterSpeedLevel", characterSpeedLevel },
                { "healingAmountLevel", healingAmountLevel }
            };
            db.Collection("users").Document(user.UserId).Collection("upgrades").Document("levels").SetAsync(upgrades);

            Dictionary<string, object> settingsFields = new Dictionary<string, object>
            {
                { "volume", true } 
            };
            db.Collection("users").Document(user.UserId).Collection("settings").Document("details").SetAsync(settingsFields);
                        
            SceneManager.LoadScene("MainMenu");
        });
    }
}





