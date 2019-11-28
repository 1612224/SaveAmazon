using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public AudioSource RunningInGrassSound;
    public AudioSource PumpingWaterSound;
    
    public void PlayRunningSound()
    {
        if (!RunningInGrassSound.isPlaying)
        {
            RunningInGrassSound.Play();
        }
        
    }

    public void PlayPumpingWaterSound()
    {
        if (!PumpingWaterSound.isPlaying)
        {
            PumpingWaterSound.Play();
        }
        
    }
}
