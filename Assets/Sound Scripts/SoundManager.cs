using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField]private AudioMixerGroup musicGroup;
    [SerializeField]private AudioMixerGroup sfxGroup;
    [SerializeField]private List<Sound> musicTracks;
    [SerializeField]private List<Sound> sfxClips;

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

        foreach (var clip in this.sfxClips)
        {
            clip.audioSource = this.gameObject.AddComponent<AudioSource>();
            clip.audioSource.clip = clip.clip;
            clip.audioSource.volume = clip.volume;
            clip.audioSource.pitch = clip.pitch;
            clip.audioSource.loop = clip.loop;
            clip.audioSource.outputAudioMixerGroup = this.sfxGroup;
        }
        //play placeholdere track
        this.PlayMusic("In Game");
    }

    public void PlayMusic(string name)
    {
        var track = this.musicTracks.Find(track => track.name == name);

        if (name == null)
        {
            Debug.Log("Sound not found: " + name);
            return;
        }

        track.audioSource.Play();
    }

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
