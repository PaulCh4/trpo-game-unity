using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [HideInInspector] public int level;
    int experience = 0;
    public ExperienceBar expBar;

    int TO_LEVEL_UP
    {
        get{
            return level * 10000;
        }
    }

    private void Start(){
        expBar.UpdateExpSlider(experience, TO_LEVEL_UP);
        expBar.SetLvelText(level);

        //level = PlayerPrefs.GetInt("level");
        Debug.Log(level);
    }


    public void AddExpirience(int amount)
    {
        experience += amount;
        CheckLevelUp();

        expBar.UpdateExpSlider(experience, TO_LEVEL_UP);
    }

    public void CheckLevelUp(){
        if(experience >= TO_LEVEL_UP){
            experience -= TO_LEVEL_UP;
            level+=1;

            expBar.SetLvelText(level);
        }
    }
}
