using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private List<Sound> musicTracks;
    private string currentScene;
    private string lastScene;
    public static SoundManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitializeMusicTracks();
        PlayMusicByScene();
    }
    void Update()
    {
        // Check if the scene has changed
        if (currentScene != SceneManager.GetActiveScene().name)
        {
            lastScene = currentScene;
            PlayMusicByScene();
        }
    }

    // Initializes each track
    void InitializeMusicTracks()
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
    }

    void PlayMusicByScene()
    {
        // Determine the current scene name
        currentScene = SceneManager.GetActiveScene().name;

        // Play the appropriate music based on the scene
        if (currentScene == "Main Menu")
        {
            StopMusic("In Game");
            PlayMusic("In Menu");
        }
        else if (currentScene == "ThirdPersonTest")
        {
            StopMusic("In Menu");
            PlayMusic("In Game");
        }
    }

    // Plays the desired Song 
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

    // Stops playing the desired Song 
    public void StopMusic(string name)
    {
        var track = this.musicTracks.Find(track => track.name == name);

        if (track == null)
        {
            Debug.Log("Sound not found: " + name);
            return;
        }

        track.audioSource.Stop();
    }
}
