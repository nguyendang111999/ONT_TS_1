
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Stop Audio Action")]
public class StopAudioActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new StopAudioAction();
}

public class StopAudioAction : StateAction
{
    private AudioManager _characterAudio;

    public override void Awake(StateController stateController)
    {
        _characterAudio = stateController.GetComponent<AudioManager>();
    }

    public override void OnStateEnter()
    {
        _characterAudio.StopAudio();
    }
    public override void OnStateUpdate()
    {
    }
}
