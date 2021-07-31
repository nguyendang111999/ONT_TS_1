using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Play Audio Action")]
public class PlayAudioActionSO : StateActionSO
{
    [SerializeField] private string _audioName;
    [SerializeField] private AudioSO _audioSO;
    public string AudioName => _audioName;
    public AudioSO AudioSource => _audioSO;

    protected override StateAction CreateAction() => new PlayAudioAction();
}

public class PlayAudioAction : StateAction
{
    PlayAudioActionSO _originSO => (PlayAudioActionSO)base.OriginSO;
    private string _audio;
    private AudioSO _audioSO;
    private ThachSanhAudio _characterAudio;

    public override void Awake(StateController stateController)
    {
        _audioSO = _originSO.AudioSource;
        _characterAudio = stateController.GetComponent<ThachSanhAudio>();
    }
    public override void OnStateEnter()
    {
        _audioSO.Source.Play();
    }
    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        _audioSO.Source.Stop();
    }
}


