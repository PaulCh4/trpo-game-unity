using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject toSpawn;
    [Range(0f,1f)] public float chance;

    public void Spawn(){
        if(Random.value < chance){
            GameObject go = Instantiate(toSpawn, transform.position, Quaternion.identity);
        }
    }
}
