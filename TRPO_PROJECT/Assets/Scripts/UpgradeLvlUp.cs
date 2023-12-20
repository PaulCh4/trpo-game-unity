using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Auth;




public class UpgradeLvlUp : MonoBehaviour
{
    public string upgradeName;
    public TextMeshProUGUI lvlText;
    public TextMeshProUGUI priceText;
    public GameObject targetObject;

    [HideInInspector]
    public static Dictionary<string, object> upgrades;
    string lvl;
    int price;
    int playerCoins;

    FirebaseUser user;
    FirebaseFirestore db;

    void Start()
    {
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        db = FirebaseFirestore.DefaultInstance;
    }

    void Update()
    {   
        upgrades = targetObject.GetComponent<RecordsTableUI>().upgrades;
        playerCoins = (int)(float)targetObject.GetComponent<RecordsTableUI>().coinsAll;
        
        if (upgrades.ContainsKey(upgradeName))
        {
            lvl = upgrades[upgradeName].ToString();
            lvlText.text = "lvl: " + lvl;
            price = int.Parse(lvl) * 10;

            priceText.text = (price).ToString();
        }
        else
        {
            Debug.Log("Upgrade not found!");
        }
    }

    public void LevelUp()
    {   
        if (playerCoins > price)
        {   
            Debug.Log(playerCoins);
            playerCoins = playerCoins - price;
            Debug.Log(playerCoins);

            lvl = (int.Parse(lvl) + 1).ToString();
            upgrades[upgradeName] = lvl;

            db.Collection("users").Document(user.UserId).Collection("upgrades").Document("levels").SetAsync(upgrades);
            Dictionary<string, object> userFields = new Dictionary<string, object>
            {
                { "coinsAll", playerCoins }
            };
            db.Collection("users").Document(user.UserId).Collection("userInfo").Document("details").UpdateAsync(userFields); }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
