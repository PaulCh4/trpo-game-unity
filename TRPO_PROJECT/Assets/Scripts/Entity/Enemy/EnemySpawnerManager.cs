using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public Vector2 spawnArea;
    public float spawnTimer;
    float timer;

    public GameObject player;

    private void Update(){
        timer -= Time.deltaTime;

        if(timer < 0f){
            Vector3 position = new Vector3();
            
            float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
            if (UnityEngine.Random.value > 0.5f)
            {
                position.x = UnityEngine.Random.Range(-spawnArea.x,spawnArea.x);
                position.y = spawnArea.y * f;            
            } else {
                position.y = UnityEngine.Random.Range(-spawnArea.y,spawnArea.y);
                position.x = spawnArea.x * f;
            }
            position.z = 0;

            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.position = player.transform.position + position;

            // Устанавливаем случайный размер для врага
            float randomSize = UnityEngine.Random.Range(0.7f, 1.3f);
            newEnemy.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

            // Устанавливаем размер коллайдера и урон в зависимости от размера врага
            newEnemy.GetComponent<EnemyScript>().SetAttributes(randomSize);

            newEnemy.GetComponent<EnemyScript>().SetTarget(player);
            newEnemy.transform.parent = transform;//?

            timer = spawnTimer;
        }
        
    }
}



        