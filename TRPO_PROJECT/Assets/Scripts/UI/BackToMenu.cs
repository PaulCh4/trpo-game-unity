using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Extensions;

public class BackToMenu : MonoBehaviour
{
    Dictionary<string, object> playerDataDict;
    public GameObject player;

    FirebaseUser user;
    FirebaseFirestore db;
    DocumentReference docRef;

    [HideInInspector]
    public int coinAcquired;
    private int level;
    private int enemiesKilled;
    private float distanceTravelled;

 
    public void CloaseGameplay(){
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        db = FirebaseFirestore.DefaultInstance;
        docRef = db.Collection("users").Document(user.UserId);

        coinAcquired = coinAcquired + player.GetComponent<Coins>().coinAcquired;
        level = player.GetComponent<Level>().level;
        enemiesKilled = PlayerPrefs.GetInt("enemiesKilled");
        distanceTravelled = PlayerPrefs.GetFloat("distanceTravelled");

        Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { "level", level },
            { "enemiesKilled", enemiesKilled },
            { "distanceTravelled", FieldValue.Increment(distanceTravelled) },
            
            { "coinsAll", FieldValue.Increment(coinAcquired) }
        };

        docRef.Collection("userInfo").Document("details").UpdateAsync(updates).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to update document: " + task.Exception);
            }
            else
            {
                Debug.Log("Document updated");

                SceneManager.LoadScene("MainMenu");
            }
        });
    }
}
