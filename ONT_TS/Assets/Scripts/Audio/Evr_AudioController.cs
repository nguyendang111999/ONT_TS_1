using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Evr_AudioController : MonoBehaviour
{
    [SerializeField]
    ObjectPositionSO playerPos;

    [SerializeField]
    AudioSO audioSO;

    [Tooltip("Distance to start playing audio")]
    [SerializeField]
    FloatValueSO Distance;

    float distance;
    AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        distance = Distance.Value;
    }

    private void FixedUpdate()
    {
        StartAudio();
    }

    void StartAudio()
    {
        if (playerPos.GetDistance(transform.position) < distance)
        {
            SetupAudio(audioSO);
        }
        else{
            audioSource.Stop();
        } 
    }

    private void SetupAudio(AudioSO audioSO)
    {
        if(audioSource.clip != null && audioSource.isPlaying) return;
        else if(audioSource.clip == null)
        {
            audioSource.clip = audioSO.Clip;
            audioSource.volume = this.audioSO.Volume;
            audioSource.pitch = audioSO.Pitch;
            audioSource.loop = audioSO.IsLoop;
            audioSource.Play();
        }
        else if(!audioSource.isPlaying && audioSO.IsLoop){
            audioSource.Play();
        }
    }

    void StopAudio()
    {
        if(!audioSource.isPlaying) return;
        if (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime;
            Debug.Log("Volume: " + audioSource.volume);
        }
        else{
            audioSource.Stop();
        }
    }

    #if UNITY_EDITOR
    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
    #endif
}
