using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    public Transform bar;
    public void SetState(int current, int max){
        float state = (float)current;//0.182682
        state/=max;
        if (state < 0f) {state = 0f;}
        bar.transform.localScale = new Vector3(state * 0.182682f, 4.617446f, 1f);
    }
}
