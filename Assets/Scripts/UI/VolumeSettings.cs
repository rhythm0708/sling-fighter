using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private string masterVolumeKey = "MasterVolume";
    private string musicVolumeKey = "MusicVolume";
    private string sfxVolumeKey = "SFXVolume";

    private void Start()
    {
        // Load previous volume settings
        masterSlider.value = PlayerPrefs.GetFloat(masterVolumeKey, 0f);
        musicSlider.value = PlayerPrefs.GetFloat(musicVolumeKey, 0f);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxVolumeKey, 0f);
        
        // Apply loaded volume settings
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        mainMixer.SetFloat("MasterVol", volume);
        PlayerPrefs.SetFloat(masterVolumeKey, volume);
        
        // Save the value immediately
        PlayerPrefs.Save(); 
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mainMixer.SetFloat("MusicVol", volume);
        PlayerPrefs.SetFloat(musicVolumeKey, volume);

        // Save the value immediately
        PlayerPrefs.Save(); 
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mainMixer.SetFloat("SFXVol", volume);
        PlayerPrefs.SetFloat(sfxVolumeKey, volume);

        // Save the value immediately
        PlayerPrefs.Save();
    }
}
