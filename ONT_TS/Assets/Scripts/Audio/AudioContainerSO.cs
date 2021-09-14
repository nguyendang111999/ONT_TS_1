using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/AudioContainer")]
public class AudioContainerSO : ScriptableObject
{
    [SerializeField] private AudioSO[] _audioList;
    public AudioSO[] AudioList => _audioList;
}
