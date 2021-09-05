using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Fox/Conditions/Is Reaching Last Location")]
public class Fox_IsLastLocationSO : StateConditionSO
{
    protected override Condition CreateCondition() => new Fox_IsLastLocation();
}

public class Fox_IsLastLocation : Condition
{
    private NPCController _controller;
    private PathSO _path;

    public override void Awake(StateController stateController){
        _controller = stateController.GetComponent<NPCController>();
        _path = _controller.Paths;
    }

    protected override bool Statement()
    {
        return _path.IsLastLocation();
    }
}
