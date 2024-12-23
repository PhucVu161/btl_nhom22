using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    public bool loop;

    public AudioMixerGroup audioMixerGroup;
    [HideInInspector]
    public AudioSource audioSource;
}
