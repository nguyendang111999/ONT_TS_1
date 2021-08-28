using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Play Audio Action")]
public class PlayAudioActionSO : StateActionSO
{
    [SerializeField] private AudioSO _audioSO;
    public AudioSO AudioSource => _audioSO;

    protected override StateAction CreateAction() => new PlayAudioAction();
}

public class PlayAudioAction : StateAction
{
    PlayAudioActionSO _originSO => (PlayAudioActionSO)base.OriginSO;
    private AudioSO _audioSO;
    private AudioManager _characterAudio;

    public override void Awake(StateController stateController)
    {
        _characterAudio = stateController.GetComponent<AudioManager>();
        _audioSO = _originSO.AudioSource;
    }
    public override void OnStateEnter()
    {
        _characterAudio.PlayAudio(_audioSO);
    }
    public override void OnStateUpdate() { }

    public override void OnStateExit()
    {
        _characterAudio.StopAudio();
    }
}


