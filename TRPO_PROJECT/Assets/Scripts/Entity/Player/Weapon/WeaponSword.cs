using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwordScript : MonoBehaviour
{
    public float timeToAttack = 4f;
    float timer;


    public GameObject leftAttackObject;
    public GameObject rightAttackObject;

    MovePlayerScript playerMove;
    public Vector2 Size = new Vector2(4f,2f);
    public int swordDamage = 1;

    void Start(){
        playerMove = GetComponentInParent<MovePlayerScript>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
        timer = timeToAttack;

        if (playerMove.lastVectorH> 0){
            rightAttackObject.SetActive(true);
            Collider2D [] hitObjects = Physics2D.OverlapBoxAll(rightAttackObject.transform.position, Size, 0f);
            ApplyDamage(hitObjects);
        } else 
        if(playerMove.lastVectorH < 0){
            leftAttackObject.SetActive(true);
            Collider2D [] hitObjects = Physics2D.OverlapBoxAll(leftAttackObject.transform.position, Size, 0f);
            ApplyDamage(hitObjects);
        }
    }

    private void ApplyDamage(Collider2D[] hitObjects){
        for (int i = 0; i < hitObjects.Length; i++){
            IDamageble e = hitObjects[i].GetComponent<IDamageble>();
            if (e != null){
                e.TakeDamage(swordDamage);
            }
        }
    }


}
