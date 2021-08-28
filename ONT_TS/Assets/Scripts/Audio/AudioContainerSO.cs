using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Audio/AudioContainer")]
public class AudioContainerSO : ScriptableObject
{
    [SerializeField] private AudioSO[] _audioList;
    public AudioSO[] AudioList => _audioList;

    public AudioSO GetAudio(string name)
    {
        return Array.Find(_audioList, audio => audio.Name == name);;
    }
}
