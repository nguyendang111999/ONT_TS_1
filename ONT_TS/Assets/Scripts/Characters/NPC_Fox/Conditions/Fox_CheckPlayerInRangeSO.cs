using System.Collections;
using System.Collections.Generic;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Check Player In Range", menuName = "State Machines/Fox/Conditions/Check If Player In Range")]
public class Fox_CheckPlayerInRangeSO : StateConditionSO
{
    [SerializeField] private float _range;
    public float Range => _range;
    protected override Condition CreateCondition() => new Fox_CheckPlayerInRange();
}

public class Fox_CheckPlayerInRange : Condition
{
    private Fox_CheckPlayerInRangeSO _originSO => (Fox_CheckPlayerInRangeSO)base.OriginSO;
    private NPCController _controller;
    private float _range;

    public override void Awake(StateController stateController){
        _controller = stateController.GetComponent<NPCController>();
        _range = _originSO.Range;
    }

    protected override bool Statement()
    {
        return _controller.GetDistanceToPlayer() < _range;
    }
}
