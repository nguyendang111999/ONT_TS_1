using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Crouch Press Condition")]
public class IsCrouchPressConditionSO : StateConditionSO<IsCrouchPressCondition> { }
public class IsCrouchPressCondition : Condition
{
    private PlayerController _ThachSanh;

    public override void Awake(StateController stateController)
    {
        _ThachSanh = stateController.GetComponent<PlayerController>();
    }
    protected override bool Statement()
    {
        return _ThachSanh.IsCrouching;
    }
}