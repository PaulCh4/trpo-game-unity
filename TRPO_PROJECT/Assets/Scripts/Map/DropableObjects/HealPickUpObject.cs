using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HealPickUpObject : MonoBehaviour, IPickUpObject
{
    //Upgrades
    private int healAmount = 50 * int.Parse(UpgradeLvlUp.upgrades["healingAmountLevel"].ToString());

    public void OnPickUp(StatisticPlayerScript player){
        player.Heal(healAmount);
    }
}
