using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField]
    private BackgroundMusicHolderSO _holder;
    private AudioSO _currentAudio;
    private AudioSource aSource;

    private void Awake()
    {
        aSource = gameObject.GetComponent<AudioSource>();
        aSource.Play();
        _currentAudio = _holder.AudioToPlay;
        SetupAudio(_currentAudio);
    }

    private void Update()
    {
        Debug.Log("Is playing: " + aSource.isPlaying);
        if (!_holder.CompareName(_currentAudio.Name))
        {
            _currentAudio = _holder.AudioToPlay;
            SetupAudio(_currentAudio);
            aSource.Play();
        }
    }

    private void SetupAudio(AudioSO audioSO)
    {
        if (audioSO == null) return;
        else
        {
            if(aSource.isPlaying) aSource.Stop();
            aSource.clip = _currentAudio.Clip;
            aSource.loop = _currentAudio.IsLoop;
            aSource.volume = _currentAudio.Volume;
            aSource.pitch = _currentAudio.Pitch;
            aSource.Play();
        }
    }
}
