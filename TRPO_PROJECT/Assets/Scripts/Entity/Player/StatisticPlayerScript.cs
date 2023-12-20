using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class StatisticPlayerScript : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp = 100;
    public StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;

    [HideInInspector]public bool isDead; 

    private void Awake(){
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();

        level.level = PlayerPrefs.GetInt("level");
    }

    private void Start(){
        hpBar.SetState(currentHp,maxHp);
    }

    public void TakeDamage(int damage){
        if(isDead == true){return;}//============DEAD

        currentHp -= damage;

        if (currentHp <= 0)
        {
            GetComponent<GameOver>().CloseGame();
            isDead = true;  
        }
        hpBar.SetState(currentHp,maxHp);
    }

    public void Heal(int amount){
        if(currentHp <= 0){return;}

        currentHp += amount;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        hpBar.SetState(currentHp,maxHp);
    }
}
