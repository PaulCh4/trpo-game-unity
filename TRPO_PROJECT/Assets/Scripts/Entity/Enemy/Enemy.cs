using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageble
{
    Transform targetPlayer;
    GameObject targetGameObject;
    StatisticPlayerScript Player;
    public float speed;
    Rigidbody2D rigidbody2D;


    public int hp = 2;
    public int damage = 5;
    public int experience_drop =  350;

    private SpriteRenderer spriteRender;

    private void Awake(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        //targetGameObject = targetPlayer.gameObject;
    }

    public void SetTarget(GameObject target){
        targetGameObject = target;
        targetPlayer = target.transform;
    }

    private void FixedUpdate(){
        Vector3 direction = (targetPlayer.position - transform.position).normalized;
        rigidbody2D.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    private void Attack(){
         if(Player == null){
            Player = targetGameObject.GetComponent<StatisticPlayerScript>();
         }
         
         Player.TakeDamage(damage);
    }


    
    private IEnumerator ResetColorAfterTime(float time){
        yield return new WaitForSeconds(time);
        spriteRender.color = Color.white;
    }
    public void TakeDamage(int damage){
            hp -=damage;

            Debug.Log(hp);
            spriteRender.color = Color.red;
            StartCoroutine(ResetColorAfterTime(1f));

            if(hp < 1)
            {
                targetGameObject.GetComponent<Level>().AddExpirience(experience_drop);
                Destroy(gameObject);
                


                 if (PlayerPrefs.HasKey("enemiesKilled"))
                {
                    // Ключ существует
                    int enemiesKilled = PlayerPrefs.GetInt("enemiesKilled");
                    PlayerPrefs.SetInt("enemiesKilled", enemiesKilled + 1);
                }
                else
                {
                    // Ключ не существует
                    PlayerPrefs.SetInt("enemiesKilled", 1);
                }
            }
        
    }

    public void SetAttributes(float size){
        // Устанавливаем размер коллайдера в зависимости от размера врага
        GetComponent<CircleCollider2D>().radius = size / (float)1.2;

        // Устанавливаем урон в зависимости от размера врага
        damage = Mathf.RoundToInt(size * 10);
        speed = Mathf.RoundToInt(-size + 5);
    }
}
