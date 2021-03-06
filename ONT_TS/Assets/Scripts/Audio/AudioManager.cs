using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("This element hold every audio of an object")]
    [SerializeField] AudioContainerSO _audios;
    private AudioSO[] _audioList;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    /// <summary>
    /// Play audio by audio's name
    /// </summary>
    public void PlayAudio(string name)
    {
        AudioSO a = _audios.GetAudio(name);
        if (a == null) return;
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        SetupAudio(a, _audioSource);
        _audioSource.Play();
    }

    /// <summary>
    /// Play audio by audio SO file
    /// </summary>
    public void PlayAudio(AudioSO a)
    {
        if (_audioSource.clip == a.Clip && _audioSource.isPlaying)
        {
            return;
        }
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        SetupAudio(a, _audioSource);
        _audioSource.Play();
    }

    /// <summary>
    /// Use by state machine
    /// </summary>
    public void StopAudio()
    {
        _audioSource.Stop();
    }
    public void StopAudio(AudioSO a)
    {
        if (_audioSource.clip == a.Clip && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    private void SetupAudio(AudioSO audioSO, AudioSource source)
    {
        source.clip = audioSO.Clip;
        source.loop = audioSO.IsLoop;
        source.volume = audioSO.Volume;
        source.pitch = audioSO.Pitch;
    }
}
