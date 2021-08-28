using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Condition/IsGrounded")]
public class IsGroundConditionSO : StateConditionSO<IsGroundCondition>{}
public class IsGroundCondition : Condition
{
    private PlayerController _controller;
    public override void Awake(StateController stateController)
    {
        _controller = stateController.GetComponent<PlayerController>();
    }
    protected override bool Statement() => _controller.IsGrounded;
}
