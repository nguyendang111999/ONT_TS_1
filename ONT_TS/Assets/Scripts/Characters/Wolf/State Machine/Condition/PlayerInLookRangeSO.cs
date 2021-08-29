using System.Collections.Generic;
using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayerIsInZone", menuName = "State Machines/Conditions/DetectPlayer/Player In Look Range")]
public class PlayerInLookRangeSO : StateConditionSO
{
    protected override Condition CreateCondition() => new PlayerInLookRange();
}

public class PlayerInLookRange : Condition
{
    private FieldOfView _fov;
    private List<Transform> _targets;
    public override void Awake(StateController stateController){
        _fov = stateController.GetComponent<FieldOfView>();
        _targets = _fov.visibleTargets;
    }
    protected override bool Statement()
    {
        return _targets.Count > 0;
    }
}
