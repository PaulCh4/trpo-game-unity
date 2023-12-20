using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;



public class RecordsTableUI : MonoBehaviour
{   
    public Canvas canvas;
    public TextMeshProUGUI playerDataText;
    public TextMeshProUGUI UpgradeText;//!!!!
    public TextMeshProUGUI liderboardDataText;
    
    FirebaseUser user;
    FirebaseFirestore db;
    DocumentReference docRef;

    [HideInInspector]
    public int level;
    [HideInInspector]
    public int enemiesKilled;
    [HideInInspector]
    public float distanceTravelled;
    [HideInInspector]
    public float coinsAll;

    [HideInInspector]
    public Dictionary<string, object> upgrades = new Dictionary<string, object>
    {
        { "weaponSpeedLevel", 0 },
        { "characterSpeedLevel", 0 },
        { "healingAmountLevel", 0 }
    };

    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Image soundButtonImage;
    public static bool IsSoundOn = true;

    public void ToggleSound()
    {
        IsSoundOn = !IsSoundOn;
        soundButtonImage.sprite = IsSoundOn ? soundOnSprite : soundOffSprite;

        // Сохраняем новое значение звука в Firebase
        docRef.UpdateAsync(new Dictionary<string, object> { { "volume", IsSoundOn ? "on" : "off" } });
    }


    void Update()
    {
         db.Collection("users").Document(user.UserId).Collection("userInfo").Document("details").GetSnapshotAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Dictionary<string, object> userFields = snapshot.ToDictionary();
                    
                    coinsAll = (int)(long)userFields["coinsAll"];
                    UpgradeText.text = $"{coinsAll}";
                }
                else
                {
                    Debug.Log("Document does not exist!");
                }
            }
         });

    }


    void Start()
    {   
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        db = FirebaseFirestore.DefaultInstance;
        docRef = db.Collection("users").Document(user.UserId);

        //RECORDS
        db.Collection("users").Document(user.UserId).Collection("userInfo").Document("details").GetSnapshotAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Dictionary<string, object> userFields = snapshot.ToDictionary();

                    playerDataText.text = $"Level:\t\t\t {userFields["level"]}\n" +
                                    $"Enemies Killed:\t\t {userFields["enemiesKilled"]}\n" +
                                    $"Distance Travelled:\t {((float)(double)userFields["distanceTravelled"]).ToString("F2")}";

                    coinsAll = (int)(long)userFields["coinsAll"];
                    UpgradeText.text = $"{coinsAll}";
                    Debug.Log(coinsAll);

        
                    PlayerPrefs.SetInt("level", (int)(long)userFields["level"]);
                    PlayerPrefs.SetInt("enemiesKilled", (int)(long)userFields["enemiesKilled"]);
                    PlayerPrefs.SetFloat("distanceTravelled", (float)(double)userFields["distanceTravelled"]);
                }
                else
                {
                    Debug.Log("Document does not exist!");
                }
            }

            canvas.gameObject.SetActive(true);
        });
        //

        //UPGRADES
        db.Collection("users").Document(user.UserId).Collection("upgrades").Document("levels").GetSnapshotAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    upgrades = snapshot.ToDictionary();
                }
                else {Debug.Log("Document does not exist!"); }
            }
        });
        //

        //LEADERBOARD
        // Получаем ссылку на коллекцию пользователей

        
        db.Collection("leaderboard").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                QuerySnapshot snapshot = task.Result;

                liderboardDataText.text = "";
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    string email = document.GetValue<string>("email");
                    string nickname = email.Split('@')[0];
                    int level = document.GetValue<int>("level");
                    liderboardDataText.text += $"{nickname}\t\t {level}\n";
                }
            }
        });

        /*
        db.Collection("users").OrderByDescending("level").Limit(1).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            
            foreach (DocumentSnapshot document in snapshot.Documents)
            {   
                Debug.Log("(!) Document ID: " + document.Id);
                Debug.Log("(!) Document Data: " + document.ToDictionary().ToString());
                Debug.Log("(!) Email: " + document.GetValue<string>("email"));
                Debug.Log("(!) Level: " + document.GetValue<double>("level"));

                string email = document.GetValue<string>("email");
                double level = document.GetValue<double>("level");

                DocumentReference leaderboardDocRef = db.Collection("leaderboard").Document(document.Id);

                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "email", email },
                    { "level", level }
                };

                leaderboardDocRef.SetAsync(data); 
            }
            

            db.Collection("leaderboard").OrderByDescending("level").Limit(1).GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    QuerySnapshot snapshot = task.Result;

                    liderboardDataText.text = "";
                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        string email = document.GetValue<string>("email");
                        string nickname = email.Split('@')[0];
                        int level = document.GetValue<int>("level");
                        liderboardDataText.text += $"{nickname}\t\t {level}\n";
                    }
                }
            });
         
    });
    */
    //

    //SETTINGS
    docRef.Collection("settings").Document("details").GetSnapshotAsync().ContinueWithOnMainThread(task => {
        if (task.IsCompleted)
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Dictionary<string, object> settings = snapshot.ToDictionary();
                bool volumeSetting = (bool)settings["volume"];

                IsSoundOn = volumeSetting;
            }
            else
            {
                Debug.Log("Document does not exist!");
            }
        }
        soundButtonImage.sprite = IsSoundOn ? soundOnSprite : soundOffSprite;
    });
    //

    }
}
