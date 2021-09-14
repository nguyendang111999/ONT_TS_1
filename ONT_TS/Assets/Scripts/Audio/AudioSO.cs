using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/AudioClip")]
public class AudioSO : ScriptableObject
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _audio;
    [Range(0f, 1f)]
    [SerializeField] private float _volume;
    [Range(.1f, 3f)]
    [SerializeField] private float _pitch;
    [SerializeField] private bool _isLoop;

    public string Name => _name;
    public AudioClip Clip => _audio;
    public float Volume => _volume;
    public float Pitch => _pitch;
    public bool IsLoop => _isLoop;
    public AudioSource Source
    {
        get { return _source; }
        set { _source = value; }
    }
}
