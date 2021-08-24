using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioContainerSO _audios;
    private AudioSO[] _audioList;

    private void OnEnable()
    {
        SetupAudio();
    }
    private void OnDisable()
    {
        // AudioSource[] a = GetComponents<AudioSource>();
        // foreach (AudioSource audio in a)
        // {
        //     Destroy(audio);
        // }
    }
    public void PlayMelleSound1() => PlayAudio("AxeMelee1Sound");
    public void PlayMelleSound2() => PlayAudio("AxeMelee2Sound");
    public void PlayRunSound() => PlayAudio("RunningSound");

    public void PlayAudio(string name)
    {
        AudioSO a = Array.Find(_audioList, audio => audio.Name == name);
        if (!a.Source.isPlaying)
            a.Source.Play();
    }
    public void StopAudio(string name)
    {
        AudioSO a = Array.Find(_audioList, audio => audio.Name == name);
        if (a.Source.isPlaying)
            a.Source.Stop();
    }
    public void SetupAudio()
    {
        _audioList = _audios.AudioList;
        for (int i = 0; i < _audioList.Length; i++)
        {
            AudioSource[] audioSources1 = GetComponents<AudioSource>();
            if(audioSources1.Length == _audioList.Length) break;
            gameObject.AddComponent<AudioSource>();
        }
        AudioSource[] audioSources = GetComponents<AudioSource>();

        for (int i = 0; i < _audioList.Length; i++)
        {
            AudioSO a = _audioList[i];

            a.Source = audioSources[i];
            a.Source.Stop();
            audioSources[i].clip = a.Clip;
            audioSources[i].loop = a.IsLoop;
            audioSources[i].volume = a.Volume;
            audioSources[i].pitch = a.Pitch;
        }
    }
}
