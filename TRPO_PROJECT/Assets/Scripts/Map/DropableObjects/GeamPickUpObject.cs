using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeamPickUpObject : MonoBehaviour, IPickUpObject
{
    public int amount;

    public void OnPickUp(StatisticPlayerScript palyer){
        palyer.level.AddExpirience(amount);
    }
}
