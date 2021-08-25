using System;
using UnityEngine;

public class ThachSanhAudio : MonoBehaviour
{
    [SerializeField] AudioSO[] _audioList;
    void Awake()
    {
        foreach (AudioSO a in _audioList)
        {
            SetupAudio(a);
        }
    }
    public void PlayMelleSound1() => PlayAudio("AxeMelee1Sound");
    public void PlayMelleSound2() => PlayAudio("AxeMelee2Sound");
    public void PlayRunSound() => PlayAudio("RunningSound");

    public void PlayAudio(string name)
    {
        AudioSO a = Array.Find(_audioList, audio => audio.Name == name);
        if(!a.Source.isPlaying)
        a.Source.Play();
    }
    public void StopAudio(string name)
    {
        AudioSO a = Array.Find(_audioList, audio => audio.Name == name);
        if(a.Source.isPlaying)
        a.Source.Stop();
    }
    public void SetupAudio(AudioSO audio)
    {
        audio.Source = gameObject.AddComponent<AudioSource>();
        audio.Source.clip = audio.Clip;
        audio.Source.loop = audio.IsLoop;
        audio.Source.volume = audio.Volume;
        audio.Source.pitch = audio.Pitch;
    }
}
