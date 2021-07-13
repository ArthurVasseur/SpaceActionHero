using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public static AudioManager instance;
    public List<AudioObject> audioObjects;

    private void Awake()
    {
        instance = this;
        SetDefault();
    }

    private void SetDefault()
    {
        foreach (AudioObject audioObject in audioObjects) {
            if (PlayerPrefs.HasKey(audioObject.volumeName))
                mixer.SetFloat(audioObject.volumeName, Mathf.Log10(PlayerPrefs.GetFloat(audioObject.volumeName)) * 20f);
            else
                mixer.SetFloat(audioObject.volumeName, Mathf.Log10(0.5f) * 20f);
        }
    }

    public void SetVolume(float volume)
    {
        foreach (AudioObject audioObject in audioObjects) {
            if (volume == -10) {
                mixer.SetFloat(audioObject.volumeName, Mathf.Log10(audioObject.slider.value)*20f);
                PlayerPrefs.SetFloat(audioObject.volumeName, audioObject.slider.value);
            }
            else {
                mixer.SetFloat(audioObject.volumeName, volume);
                PlayerPrefs.SetFloat(audioObject.volumeName, volume);
            }
        }
    }

    public void SetVolume(float volume, string volumeName)
    {
        mixer.SetFloat(volumeName, Mathf.Log10(volume)*20f);
    }
}

[Serializable]
public class AudioObject
{
    public string volumeName;
    public Slider slider;
}