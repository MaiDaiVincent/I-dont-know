using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;

    [HideInInspector]
    public AudioSource audioSource;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0, 3f)]
    public float pitch = 1f;

    public bool loop;

    public bool mute;
}
