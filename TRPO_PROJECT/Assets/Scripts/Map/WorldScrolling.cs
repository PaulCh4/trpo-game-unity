using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    public Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0,0);
    public Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    public float tileSize = 42f;
    GameObject[,] sandTiles;

    public int sandTilesHorizontalCount;
    public int sandTilesVerticalCount;

    public int fieldOfVisionH = 3;
    public int fieldOfVisionW = 3;

    public void Awake()
    {
        sandTiles = new GameObject[sandTilesHorizontalCount, sandTilesVerticalCount];
    }

    
    public void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

        if(currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPosition.x = CalculatePositionOnAxisWithWrap(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxisWithWrap(onTileGridPlayerPosition.y, false);
            
            UpdateTilesOnScreen();
        }
    }

    public void Start()
    {
        for (int pov_x = -(fieldOfVisionW/2); pov_x <= fieldOfVisionW/2; pov_x++)
        {
            for (int pov_y = -(fieldOfVisionH/2); pov_y <= fieldOfVisionH/2; pov_y++)
            {
                int tileToUpdate_x =  CalculatePositionOnAxisWithWrap(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxisWithWrap(playerTilePosition.y + pov_y, false);

                GameObject tile = sandTiles[tileToUpdate_x, tileToUpdate_y];
            }
        }
        //UpdateTilesOnScreen();

        GameObject[,] newSandTiles = new GameObject[sandTilesHorizontalCount, sandTilesVerticalCount];

        for (int i = 0; i < sandTiles.GetLength(0); i++)
        {
            for (int j = 0; j < sandTiles.GetLength(1); j++)
            {
                int i2, j2;
                do
                {
                    i2 = UnityEngine.Random.Range(0, sandTiles.GetLength(0));
                    j2 = UnityEngine.Random.Range(0, sandTiles.GetLength(1));
                }
                while (newSandTiles[i2, j2] != null);

                newSandTiles[i2, j2] = sandTiles[i, j];
            }
        }

        sandTiles = newSandTiles;
        UpdateTilesOnScreen();
    }


    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        sandTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }

    public int CalculatePositionOnAxisWithWrap(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if(currentValue >=0)
            {
                currentValue = currentValue % sandTilesHorizontalCount;
            }
            else{
                currentValue += 1;
                currentValue = sandTilesHorizontalCount -1 + currentValue % sandTilesHorizontalCount;
            }
        }
        else{
            if(currentValue >=0)
            {
                currentValue = currentValue % sandTilesVerticalCount;
            }
            else{
                currentValue += 1;
                currentValue = sandTilesVerticalCount -1 + currentValue % sandTilesVerticalCount;
            }

        }

        return (int)currentValue;
    }

    public void UpdateTilesOnScreen(){
        for (int pov_x = -(fieldOfVisionW/2); pov_x <= fieldOfVisionW/2; pov_x++)
        {
            for (int pov_y = -(fieldOfVisionH/2); pov_y <= fieldOfVisionH/2; pov_y++)
            {
                int tileToUpdate_x =  CalculatePositionOnAxisWithWrap(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxisWithWrap(playerTilePosition.y + pov_y, false);

                GameObject tile = sandTiles[tileToUpdate_x, tileToUpdate_y];
                
                Vector3 newPosition = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
                if(newPosition !=  tile.transform.position)
                {
                    tile.transform.position = newPosition;
                    sandTiles[tileToUpdate_x, tileToUpdate_y].GetComponent<SandTileScript>().Spawn();

                }
           }
        }
    }

    public Vector3 CalculateTilePosition(int x, int y){
        return new Vector3(x * tileSize, y * tileSize, 5f);
    }
}
