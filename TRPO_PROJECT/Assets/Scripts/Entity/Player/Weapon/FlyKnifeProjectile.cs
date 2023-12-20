using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class FlyKnifeProjectile : MonoBehaviour
{
    Vector3 direction;
    public float speed;
    public float angle;
    public int damage = 2;

    public void SetDirection(float dir_x, float dir_y){
        float angleInDegrees = angle; // Угол в градусах

        if (dir_x < 0) 
        {
            angleInDegrees = 180f - angleInDegrees; // Если игрок движется влево, изменить угол
        }
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad; // Преобразовать угол в радианы

        direction = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)); // Установить направление движения

        if(dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }

    bool hitDetected = false;
    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;


        if(Time.frameCount % 6 == 0){
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.1f);
            foreach(Collider2D p in hit)
            {
                EnemyScript enemy = p.GetComponent<EnemyScript>();
                if(enemy != null)
                {
                    enemy.TakeDamage(damage);
                    hitDetected = true;
                    break;
                }       
            }
            if(hitDetected == true){
                Destroy(gameObject);
            }

        }
    }
}
