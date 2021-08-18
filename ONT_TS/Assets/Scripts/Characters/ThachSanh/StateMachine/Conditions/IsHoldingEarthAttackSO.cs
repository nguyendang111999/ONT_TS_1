
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Holding Earth Attack")]
public class IsHoldingEarthAttackSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsHoldingEarthAttack();
}

public class IsHoldingEarthAttack : Condition
{
    PlayerController _thachSanh;
    public override void Awake(StateController stateController)
    {
        _thachSanh = stateController.GetComponent<PlayerController>();
    }
    protected override bool Statement(){
        return _thachSanh.IsPerformingEarth;
    } 
}
