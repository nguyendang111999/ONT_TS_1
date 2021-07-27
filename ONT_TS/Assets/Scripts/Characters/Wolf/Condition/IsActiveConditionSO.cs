using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/IsObjectActiveContition")]
public class IsActiveConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsActiveCondition();
}

public class IsActiveCondition : Condition
{
    bool isActive = false;
    public override void Awake(StateController stateController){
        isActive = stateController.gameObject.activeSelf;
    }
    protected override bool Statement() => isActive;
}
