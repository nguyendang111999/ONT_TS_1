using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Tap Heavy Attack")]
public class IsTapHeavyAttackSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsTapHeavyAttack();
}
public class IsTapHeavyAttack : Condition
{
    PlayerController _thachSanh;

    public override void Awake(StateController stateController)
    {
        _thachSanh = stateController.GetComponent<PlayerController>();
    }

    protected override bool Statement() => _thachSanh.onHeavyAttack;
}
