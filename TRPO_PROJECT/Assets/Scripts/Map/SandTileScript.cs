using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SandTileScript : MonoBehaviour
{
    public Vector2Int tilePosition;
    public List<SpawnObject> spaawnObject;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);

        transform.position = new Vector3(-100, -100, 0);
    }

    public void Spawn(){
        for (int i = 0; i< spaawnObject.Count; i++){
            spaawnObject[i].Spawn();
        }
    }
}
