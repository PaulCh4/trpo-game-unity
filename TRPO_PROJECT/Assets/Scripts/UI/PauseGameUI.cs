using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseButton : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject player;

    public void PauseGame()
    {
        Debug.Log("Пауза!!");
        isPaused = !isPaused;

        if (isPaused)
        {
            // Установка игры на паузу
            Time.timeScale = 0;
            player.GetComponent<MovePlayerScript>().enabled = false;
            player.GetComponent<Animate>().enabled = false;
        }
        else
        {
            // Возобновление игры
            Time.timeScale = 1;
            player.GetComponent<MovePlayerScript>().enabled = true;
            player.GetComponent<Animate>().enabled = true;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}