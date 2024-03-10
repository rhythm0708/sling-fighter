using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField]private AudioMixerGroup musicGroup;
    [SerializeField]private List<Sound> musicTracks;

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
}