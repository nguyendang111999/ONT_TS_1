using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EndGame : MonoBehaviour
{
    [SerializeField] CameraShake _camShake;

    [Tooltip("Duration of camera shake")]
    [SerializeField] float duration;

    [Tooltip("The intensity/how strong the camera will shake")]
    [SerializeField] float magnitude;

    public AudioSO audioSO;
    AudioSource _aSource;
    PlayerController _playerController;

    private void Start()
    {
        _aSource = gameObject.GetComponent<AudioSource>();
        SetupAudio(audioSO, _aSource);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _playerController = other.gameObject.GetComponent<PlayerController>();
            StartCoroutine(_camShake.Shake(duration, magnitude, _playerController));
            if (_aSource.isPlaying) return;
            else _aSource.Play();
        }

    }

    void SetupAudio(AudioSO audioSO, AudioSource source)
    {
        source.clip = audioSO.Clip;
        source.loop = audioSO.IsLoop;
        source.volume = audioSO.Volume;
        source.pitch = audioSO.Pitch;
    }
}
