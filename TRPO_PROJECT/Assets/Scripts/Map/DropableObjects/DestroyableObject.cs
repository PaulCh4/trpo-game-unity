using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class DestroyableObject : MonoBehaviour, IDamageble
{
    public void TakeDamage(int damage){
        DestroyImmediate(gameObject);
    }
}
