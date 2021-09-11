using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField]
    private BackgroundMusicHolderSO _holder;
    private AudioSO _currentAudio;
    private AudioSource[] aSources;

    private void Start()
    {
        aSources = gameObject.GetComponents<AudioSource>();
        _currentAudio = _holder.AudioToPlay;
        SetupAudio(_currentAudio);
    }

    private void FixedUpdate()
    {
        if (_holder.NewAudioSet)
        {
            StopAudio(_currentAudio);
            _currentAudio = _holder.AudioToPlay;
            SetupAudio(_currentAudio);
            _holder.NewAudioSet = false;
        }
    }

    private void SetupAudio(AudioSO audioSO)
    {
        if(CheckIfAudioIsPlaying(audioSO)) return;
        AudioSource aSource = GetReadyAudioSource();
        if (audioSO == null) return;
        else
        {
            aSource.clip = _currentAudio.Clip;
            aSource.loop = _currentAudio.IsLoop;
            aSource.volume = _currentAudio.Volume;
            aSource.pitch = _currentAudio.Pitch;
            aSource.Play();
        }
    }    

    void StopAudio(AudioSO audioSO)
    {
        for (int i = 0; i < aSources.Length; i++)
        {
            if (aSources[i].clip == audioSO.Clip)
            {
                aSources[i].Stop();
            }
        }
    }

    /// <summary>
    /// Check if audio is already playing
    /// </summary>
    bool CheckIfAudioIsPlaying(AudioSO audio)
    {
        bool result = false;
        for (int i = 0; i < aSources.Length; i++)
        {
            if (aSources[i].clip == audio.Clip && aSources[i].isPlaying)
            {
                result = true;
                break;
            }
            else if (i == aSources.Length - 1) result = false;
        }
        return result;
    }

    /// <summary>
    /// Get the audio source that not playing any audio
    /// </summary>
    private AudioSource GetReadyAudioSource()
    {
        AudioSource aSource = new AudioSource();
        aSources = gameObject.GetComponents<AudioSource>();
        for (int i = 0; i < aSources.Length; i++)
        {
            if (!aSources[i].isPlaying)
            {
                aSource = aSources[i];
                break;
            }
            if (i == aSources.Length - 1)
            {
                gameObject.AddComponent<AudioSource>();
            }
        }
        return aSource;
    }
}
