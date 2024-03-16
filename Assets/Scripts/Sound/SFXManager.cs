using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    [SerializeField]private AudioMixerGroup sfxGroup;
    [SerializeField]private List<Sound> sfxClips;

    void Awake()
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