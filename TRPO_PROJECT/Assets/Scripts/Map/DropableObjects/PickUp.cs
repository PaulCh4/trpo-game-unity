using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{   
    //public int healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StatisticPlayerScript p = collision.GetComponent<StatisticPlayerScript>();
        if(p != null){
            //p.Heal(healAmount);
            GetComponent<IPickUpObject>().OnPickUp(p);
            Destroy(gameObject);
        }
    }
}
