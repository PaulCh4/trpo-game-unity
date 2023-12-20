using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickUpObject : MonoBehaviour, IPickUpObject
{
    public int count = 2;

    public void OnPickUp(StatisticPlayerScript player)
    {
        player.coins.Add(count);
    }
}
