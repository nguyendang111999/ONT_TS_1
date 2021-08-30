using System.Collections.Generic;
using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Is Detected Condition", menuName = "State Machines/Wolf/Conditions/Detected Target Condition")]
public class PlayerDetectedSO : StateConditionSO
{
    protected override Condition CreateCondition() => new PlayerDetected();
}

public class PlayerDetected : Condition
{
    private EnemyBehaviour _wolf;
    private FieldOfView _fov;
    private List<Transform> _targets;
    public override void Awake(StateController stateController){
        _fov = stateController.GetComponent<FieldOfView>();
        _targets = _fov.visibleTargets;
        _wolf = stateController.GetComponent<EnemyBehaviour>();
    }
    protected override bool Statement() => _targets.Count > 0;

    public override void OnStateExit()
    {
        _wolf.Target = _targets[0];
    }
}
