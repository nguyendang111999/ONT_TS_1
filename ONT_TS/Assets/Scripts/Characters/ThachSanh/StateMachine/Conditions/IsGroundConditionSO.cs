using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Condition/IsGrounded")]
public class IsGroundConditionSO : StateConditionSO<IsGroundCondition>{}
public class IsGroundCondition : Condition
{
    private PlayerController _playerController;
    public override void Awake(StateController stateController)
    {
        _playerController = stateController.GetComponent<PlayerController>();
    }
    protected override bool Statement() => _playerController.IsGrounded;
}
