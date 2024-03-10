using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private List<Sound> musicTracks;
    private static SoundManager instance;

    void Awake()
    {
        foreach (var track in this.musicTracks)
        {
            track.audioSource = this.gameObject.AddComponent<AudioSource>();
            track.audioSource.clip = track.clip;
            track.audioSource.volume = track.volume;
            track.audioSource.pitch = track.pitch;
            track.audioSource.loop = track.loop;
            track.audioSource.outputAudioMixerGroup = this.musicGroup;
        }

        // Determine the current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // Play the appropriate music based on the scene
        if (currentScene == "Main Menu")
        {
            this.PlayMusic("In Menu");
        }
        else if(currentScene == "ThirdPersonTest")
        {
            this.PlayMusic("In Game");
        }
        else if (currentScene == "Settings")
        {
            this.PlayMusic("In Game");
        }
    }

    public void PlayMusic(string name)
    {
        var track = this.musicTracks.Find(track => track.name == name);

        if (track == null)
        {
            Debug.Log("Sound not found: " + name);
            return;
        }

        track.audioSource.Play();
    }
}
