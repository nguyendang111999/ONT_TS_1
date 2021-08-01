using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Holding Heavy Attack")]
public class IsHoldingHeavyAttackSO : StateConditionSO<IsHoldingHeavyAttack> { }

public class IsHoldingHeavyAttack : Condition
{
    PlayerController _ThachSanh;

    public override void Awake(StateController stateController)
    {
        _ThachSanh = stateController.GetComponent<PlayerController>();
    }

    protected override bool Statement() => _ThachSanh.onHoldHeavyAttack;
}
