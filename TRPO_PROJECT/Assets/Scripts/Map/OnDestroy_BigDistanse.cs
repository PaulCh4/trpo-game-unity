using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroy_BigDistanse : MonoBehaviour
{
    Transform playerTransform;
    float maxDistance = 200f;

    private void Start(){
        playerTransform = GameManager.instance.playerTransform;
    }

    private void Update(){
        if (playerTransform != null) 
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if(distance > maxDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}
