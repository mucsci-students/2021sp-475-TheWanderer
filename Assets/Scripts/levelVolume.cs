using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelVolume : MonoBehaviour
{
    private AudioSource AudioSrc;

    void Awake()
    {
        AudioSrc = GetComponent<AudioSource>();    
        float wantedVolume = PlayerPrefs.GetFloat("volume");
        AudioSrc.volume = wantedVolume;
        PlayerPrefs.SetFloat("volume2", wantedVolume);
        PlayerPrefs.Save();
        
    }
}
