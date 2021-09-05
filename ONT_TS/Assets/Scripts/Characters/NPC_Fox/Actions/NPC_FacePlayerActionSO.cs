using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Fox/Actions/Face Player Action")]
public class NPC_FacePlayerActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new NPC_FacePlayerAction();
}

public class NPC_FacePlayerAction : StateAction
{
    private NPCController _controller;

    public override void Awake(StateController stateController){
        _controller = stateController.GetComponent<NPCController>();
    }

    public override void OnStateUpdate()
    {
        Transform player = _controller.PlayerPosition.Transform;
        Vector3 relativePos = player.position - _controller.transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos);
        _controller.transform.rotation = rotation;
    }
}


