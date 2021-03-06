using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Background music holder")]
public class BackgroundMusicHolderSO : ScriptableObject
{
    [SerializeField] private AudioSO _audioToPlay;
    public bool NewAudioSet = false;
    public AudioSO AudioToPlay
    {
        get { return _audioToPlay; }
        set
        {
            if (value.Clip == _audioToPlay.Clip)
                return;
            else{
                _audioToPlay = value;
                NewAudioSet = true;
            }
        }
    }
    public bool CompareName(string name)
    {
        if (_audioToPlay == null) return false;
        return _audioToPlay.Name.Equals(name);
    }
}
