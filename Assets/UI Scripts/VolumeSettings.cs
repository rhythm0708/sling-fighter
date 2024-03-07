using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    public void SetMasterVolume()
    {
        mainMixer.SetFloat("MasterVol", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        mainMixer.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        mainMixer.SetFloat("SFXVol", sfxSlider.value);
    }
}
