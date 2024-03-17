using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private List<Sound> musicTracks;
    [SerializeField] private AudioMixerGroup sfxGroup;
    [SerializeField] private List<Sound> sfxClips;

    private string currentScene;
    private string lastScene;
    public static SoundManager instance;

    void Awake()
    {
        InitializeMusicTracks();
        InitializeSFX();
        
        if (instance != null)
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

    // Initializes each sfx
    void InitializeSFX()
    {
        foreach (var clip in this.sfxClips)
        {
            clip.audioSource = this.gameObject.AddComponent<AudioSource>();
            clip.audioSource.clip = clip.clip;
            clip.audioSource.volume = clip.volume;
            clip.audioSource.pitch = clip.pitch;
            clip.audioSource.loop = clip.loop;
            clip.audioSource.outputAudioMixerGroup = this.sfxGroup;
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
        else if (currentScene == "Wave1")
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

    // Plays the desired SFX
    public void PlaySfx(string name)
    {
        var track = this.sfxClips.Find(track => track.name == name);

        if(null == track) 
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

    // Stops playing the desired SFX
    public void StopSfx(string name)
    {
        var track = this.sfxClips.Find(track => track.name == name);

        if(null == track) 
        {
            Debug.Log("Sound not found: " + name);
            return;
        }

        track.audioSource.Stop();
    }
}
