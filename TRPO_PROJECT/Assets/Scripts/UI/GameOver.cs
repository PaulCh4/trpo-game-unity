using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject player;

    public void CloseGame()
    {
        Debug.Log("Game Over!");
        GetComponent<MovePlayerScript>().enabled = false;
        gameOverPanel.SetActive(true);
        player.SetActive(false);
    }
}
