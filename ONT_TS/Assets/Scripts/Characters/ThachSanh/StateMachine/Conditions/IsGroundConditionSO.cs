using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Condition/IsGrounded")]
public class IsGroundConditionSO : StateConditionSO<IsGroundCondition>{}
public class IsGroundCondition : Condition
{
    private CharacterController _controller;
    public override void Awake(StateController stateController)
    {
        _controller = stateController.GetComponent<CharacterController>();
    }
    protected override bool Statement() => _controller.isGrounded;
}
