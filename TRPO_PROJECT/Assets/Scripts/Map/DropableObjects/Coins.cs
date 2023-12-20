using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Extensions;

public class Coins : MonoBehaviour
{
    public int coinAcquired;
    public TMPro.TextMeshProUGUI coinsCountText;

    Firebase.Auth.FirebaseUser user;

    public void Start(){
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        // Get player data from Firebase
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(user.UserId);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted && !task.IsFaulted)
            {
                DocumentSnapshot snapshot = task.Result;
 
                Dictionary<string, object> playerData = snapshot.ToDictionary();
                
                coinAcquired = (int)playerData["coins"];
            }
        });
    }

    public void Add(int count){
        coinAcquired += count;
        coinsCountText.text = coinAcquired.ToString() + " x";
    }
}
