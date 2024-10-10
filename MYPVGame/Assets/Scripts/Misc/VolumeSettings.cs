using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Audio;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private Slider _musicSlider;

    public void Start()
    {
        _SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume );
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}
