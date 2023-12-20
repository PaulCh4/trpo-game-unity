using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FlyKnife : MonoBehaviour
{
    //Upgrades
    private float timeToAttack = 3 - (0.2f * float.Parse(UpgradeLvlUp.upgrades["weaponSpeedLevel"].ToString()));
    float timer;

    MovePlayerScript playerMove;
    [SerializeField] GameObject knifePrefab;

    private void Awake(){
        playerMove = GetComponentInParent<MovePlayerScript>();
        Debug.Log("_________" + playerMove);
    }

    private void Update(){
        if(timer < timeToAttack){
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
        SpawnKnife();
    }

    private void SpawnKnife(){
        GameObject flyKnife = Instantiate(knifePrefab);

        Debug.Log(flyKnife);

        flyKnife.transform.position = transform.position;
        flyKnife.GetComponent<FlyKnifeProjectile>().SetDirection(-playerMove.lastVectorH, 1f);
    }
}
