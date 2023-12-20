using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Auth;


public class VolumeSettings : MonoBehaviour
{
    AudioSource audioSource;
    bool volumeSetting;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        volumeSetting = RecordsTableUI.IsSoundOn;
        
        if (volumeSetting == true)
        {
            audioSource.mute = false;
        }
        else if (volumeSetting == false)
        {
            audioSource.mute = true;
        }

        var user = FirebaseAuth.DefaultInstance.CurrentUser;
        var db = FirebaseFirestore.DefaultInstance;
        var docRef = db.Collection("users").Document(user.UserId).Collection("settings").Document("details");

        Dictionary<string, object> settingsFields = new Dictionary<string, object>
        {
            { "volume", volumeSetting }
        };

        docRef.UpdateAsync (settingsFields);
    }
}
