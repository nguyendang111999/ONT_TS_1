using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Background music holder")]
public class BackgroundMusicHolderSO : ScriptableObject
{
    [SerializeField] private AudioSO _audioToPlay;
    public AudioSO AudioToPlay
    {
        get { return _audioToPlay; }
        set { _audioToPlay = value; }
    }
    public bool CompareName(string name)
    {
        if(_audioToPlay == null) return false;
        return _audioToPlay.Name.Equals(name);
    }
}
