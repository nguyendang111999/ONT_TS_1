using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Being Hit Condition")]
public class IsBeingHitConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsBeingHitCondition();
}

public class IsBeingHitCondition : Condition
{
    Damageable _damageable;

    public override void Awake(StateController stateController){
        _damageable = stateController.GetComponent<Damageable>();
    }
    protected override bool Statement() {
        return _damageable != null ? _damageable.GetHit : false;
    } 
}
