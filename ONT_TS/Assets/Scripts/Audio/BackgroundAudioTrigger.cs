using UnityEngine;

public class BackgroundAudioTrigger : MonoBehaviour
{
    [Tooltip("Background music holder")]
    [SerializeField] 
    private BackgroundMusicHolderSO _backgroundMusicHolder;

    [Tooltip("Audio to play")]
    [SerializeField] 
    private AudioSO _audio;

    private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Player")){
            _backgroundMusicHolder.AudioToPlay = _audio;
        }
    }
}
