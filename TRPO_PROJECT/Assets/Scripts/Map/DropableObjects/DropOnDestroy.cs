using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    public GameObject itemPrefab; // Предмет для выпадения
    public float dropChance; // Шанс выпадения
}

public class DropOnDestroy : MonoBehaviour
{
    public DropItem[] dropItems; // Массив предметов для выпадения
    bool isQuitting = false;

    private void OnApplicationQuit(){
        isQuitting = true;
    }

    public void OnDestroy(){
         if(isQuitting || !this.gameObject.scene.isLoaded){return;}

        // Вычисляем общий шанс выпадения
        float totalChance = 0f;
        foreach (DropItem dropItem in dropItems)
        {
            totalChance += dropItem.dropChance;
        }

        // Выбираем случайное число
        float randomDrop = UnityEngine.Random.Range(0, totalChance);

        // Определяем, какой предмет выпадет
        foreach (DropItem dropItem in dropItems)
        {
            if (randomDrop < dropItem.dropChance)
            {
                 if (dropItem.itemPrefab != null) 
                {
                    // Создаем выпадающий предмет
                    Transform t = Instantiate(dropItem.itemPrefab).transform;
                    t.position = transform.position;
                }
                break;
            }
            randomDrop -= dropItem.dropChance;
        }
    }
}
