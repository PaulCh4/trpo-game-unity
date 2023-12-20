using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class ResetProgress : MonoBehaviour
{   
    FirebaseUser user;
    FirebaseFirestore db;


    public void Reset()
    {   
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        db = FirebaseFirestore.DefaultInstance;

        // Создайте словарь для обновления
        Dictionary<string, object> userFields = new Dictionary<string, object>
        {
            { "level", 1 },
            { "enemiesKilled", 0 },
            { "distanceTravelled", 0f },
            { "coinsAll", 0 }
        };
        db.Collection("users").Document(user.UserId).Collection("userInfo").Document("details").UpdateAsync(userFields);

        Dictionary<string, object> upgrades = new Dictionary<string, object>
        {
            { "weaponSpeedLevel", 1 },
            { "characterSpeedLevel", 1 },
            { "healingAmountLevel", 1 }
        };
        db.Collection("users").Document(user.UserId).Collection("upgrades").Document("levels").UpdateAsync(upgrades);

        SceneManager.LoadScene("MainMenu");
    }
}
