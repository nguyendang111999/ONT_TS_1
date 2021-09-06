using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Play Audio When Close Player", menuName = "State Machines/Fox/Actions/Play Audio When Close Player")]
public class PlaySoundWhenClosePlayerSO : StateActionSO
{
    [SerializeField] private float distance = default;
    public float Distance => distance;
    [SerializeField] private AudioSO _audioSO;
    public AudioSO AudioSource => _audioSO;
    protected override StateAction CreateAction() => new PlaySoundWhenClosePlayer();
}

public class PlaySoundWhenClosePlayer : StateAction
{
    PlaySoundWhenClosePlayerSO _originSO => (PlaySoundWhenClosePlayerSO)base.OriginSO;
    private AudioSO _audioSO;
    private AudioManager _audioManager;
    private NPCController _controller;
    float _distance;

    public override void Awake(StateController stateController)
    {
        _audioManager = stateController.GetComponent<AudioManager>();
        _audioSO = _originSO.AudioSource;
        _controller = stateController.GetComponent<NPCController>();
        _distance = _originSO.Distance;
    }
    public override void OnStateUpdate()
    {
        if (_controller.GetDistanceToPlayer() < _distance)
        {
            _audioManager.PlayAudio(_audioSO);
        }
        else{
            _audioManager.StopAudio(_audioSO);
        }
    }
}
