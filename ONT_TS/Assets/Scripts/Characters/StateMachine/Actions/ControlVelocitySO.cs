using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

public class ControlVelocitySO : StateActionSO<ControlVelocity> { }
public class ControlVelocity : StateAction
{
    private float _velocity;
    private PlayerMovementBehaviour _movementBehaviour;

    public override void Awake(StateController stateController)
    {
        _movementBehaviour = stateController.GetComponent<PlayerMovementBehaviour>();
    }
    public override void OnStateUpdate()
    {

    }
}
