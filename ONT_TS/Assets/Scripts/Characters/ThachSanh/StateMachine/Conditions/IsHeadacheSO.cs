using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Condition/IsHeadache")]
public class IsHeadacheSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsHeadache();
}

public class IsHeadache : Condition
{
    PlayerController _controller;

    public override void Awake(StateController stateController){
        _controller = stateController.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        return _controller.Headache;
    }
}
