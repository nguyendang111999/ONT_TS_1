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
    public void PlayMelleSound1() => PlayAudio("AxeMelee1Sound");
    public void PlayMelleSound2() => PlayAudio("AxeMelee2Sound");
    public void PlayRunSound() => PlayAudio("RunningSound");

    public void PlayAudio(string name)
    {
        AudioSO a = _audios.GetAudio(name);
        if(a == null) return;
        if (_audioSource.isPlaying){
            _audioSource.Stop();
        }
        SetupAudio(a, _audioSource);
        _audioSource.Play();
    }
    public void PlayAudio(AudioSO a)
    {
        if (_audioSource.isPlaying){
            _audioSource.Stop();
        }
        SetupAudio(a, _audioSource);
        _audioSource.Play();
    }
    public void StopAudio()
    {
        _audioSource.Stop();
    }

    public void SetupAudio(AudioSO audioSO, AudioSource source){
        source.clip = audioSO.Clip;
        source.loop = audioSO.IsLoop;
        source.volume = audioSO.Volume;
        source.pitch = audioSO.Pitch;
    }
}
