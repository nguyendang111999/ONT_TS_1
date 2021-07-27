using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Dead Condition")]
public class IsDeadConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsDeadCondition();
}

public class IsDeadCondition : Condition
{
    private Damageable _damageable;

    public override void Awake(StateController stateController){
        _damageable = stateController.GetComponent<Damageable>();
    }
    protected override bool Statement() => _damageable.isDead;
}
