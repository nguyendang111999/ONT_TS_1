using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Holding Attack")]
public class IsHoldingAttackSO : StateConditionSO<IsHoldingAttack> { }
public class IsHoldingAttack : Condition
{
    private PlayerController _ThachSanh;
    public override void Awake(StateController stateController)
    {
        _ThachSanh = stateController.GetComponent<PlayerController>();
    }
    protected override bool Statement() => _ThachSanh.attackInput;

}
