using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider slider;
    public TMPro.TextMeshProUGUI levelText;

    public void UpdateExpSlider(int current, int target)
    {
        slider.maxValue = target;
        slider.value = current;
    }

    public void SetLvelText(int level){
        levelText.text = "Lvl: " + level.ToString();
    }
}
