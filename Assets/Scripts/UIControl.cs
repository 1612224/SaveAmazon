using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIControl : MonoBehaviour
{
    public static float timer;
    public TextMeshProUGUI timerText;

    public static bool IsGamePaused = false;
    public GameObject PauseMenuUI;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsGamePaused)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }
}
