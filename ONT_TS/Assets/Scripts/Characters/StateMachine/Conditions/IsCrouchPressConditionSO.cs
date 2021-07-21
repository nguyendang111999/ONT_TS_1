using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Crouch Press Condition")]
public class IsCrouchPressConditionSO : StateConditionSO<IsCrouchPressCondition> { }
public class IsCrouchPressCondition : Condition
{
    private PlayerController _playerController;

    public override void Awake(StateController stateController)
    {
        _playerController = stateController.GetComponent<PlayerController>();
    }
    protected override bool Statement()
    {
        Debug.Log("isCrouching: " + _playerController.isCrouching);
        return _playerController.isCrouching;
    }
}