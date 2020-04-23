using System;
using Toastapp.DesignPatterns;
using UnityEngine;

public class AudioManager : SceneSingleton<AudioManager>
{
    [SerializeField]
    private AudioClip moveSound;

    [SerializeField]
    private AudioSource audioSource;

    public void PlayOneShot(AudioClip audioClip)
    {
        this.audioSource.PlayOneShot(audioClip);
    }

    public void PlayMove()
    {
        this.audioSource.PlayOneShot(this.moveSound);
    }
}
