using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{
    public static float timer;
    public TextMeshProUGUI timerText;
    private void FixedUpdate()
    {
        timer = Mathf.Round(DamageCircle.shrinkTimer);
        if(timer > 0)
        {
            timerText.text = timer.ToString("0");
        }
        else
        {
            timerText.text = "0";
        }
        
    }
}
