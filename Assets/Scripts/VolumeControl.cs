using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioSource backgroundMusic;

    public void SetLevel(float sliderValue)
    {
        backgroundMusic.volume = sliderValue;
    }
}
