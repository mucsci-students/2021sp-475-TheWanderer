using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    private AudioSource AudioSrc;
    private float AudioVolume;

    // Start is called before the first frame update
    void Start()
    {
        AudioSrc = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if( PlayerPrefs.GetFloat("volume2") != 0.0f)
        {
            AudioSrc.volume = PlayerPrefs.GetFloat("volume2");
        }
        else
        {
            AudioSrc.volume = AudioVolume;
            PlayerPrefs.SetFloat("volume", AudioVolume);
        }

    }

    public void SetVolume(float vol)
    {
        
            AudioVolume = vol;
    }
}
