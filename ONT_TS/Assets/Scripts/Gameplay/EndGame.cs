using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(AudioSource))]
public class EndGame : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;
    [SerializeField] CameraShake _camShake;

    [Tooltip("Duration of camera shake")]
    [SerializeField] float duration;

    [Tooltip("The intensity/how strong the camera will shake")]
    [SerializeField] float magnitude;

    public AudioSO audioSO;
    AudioSource _aSource;
    PlayerController _playerController;
    [SerializeField] PlayableDirector CameraDirector;

    [SerializeField] GameObject _tiger;
    [SerializeField] GameObject _TSHud;

    private void Start()
    {
        _aSource = gameObject.GetComponent<AudioSource>();
        SetupAudio(audioSO, _aSource);
        _tiger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _tiger.SetActive(true);
            _playerController = other.gameObject.GetComponent<PlayerController>();
            _TSHud.SetActive(false);
            CameraDirector.Play();
            StartCoroutine(_camShake.Shake(duration, magnitude, _playerController));
            // StartCoroutine(CutScene());
            if (_aSource.isPlaying) return;
            else _aSource.Play();
        }

    }

    IEnumerator CutScene(){
        CameraDirector.Play();
        StartCoroutine(_camShake.Shake(duration, magnitude, _playerController));
        yield return new WaitForSeconds(20f);
    }

    void SetupAudio(AudioSO audioSO, AudioSource source)
    {
        source.clip = audioSO.Clip;
        source.loop = audioSO.IsLoop;
        source.volume = audioSO.Volume;
        source.pitch = audioSO.Pitch;
    }
}
