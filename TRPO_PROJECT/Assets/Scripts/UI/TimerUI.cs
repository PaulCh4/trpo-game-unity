using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    public GameObject player;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int) t / 60).ToString("00");
        string seconds = ((int) t % 60).ToString("00");

        if(player.GetComponent<StatisticPlayerScript>().isDead == false){timerText.text = minutes + ":" + seconds;}
    }
}
