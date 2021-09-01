using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Is Detected Condition", menuName = "State Machines/Wolf/Conditions/Detected Target Condition")]
public class PlayerDetectedSO : StateConditionSO
{
    protected override Condition CreateCondition() => new PlayerDetected();
}

public class PlayerDetected : Condition
{
    private FieldOfView _fov;
    private EnemyBehaviour _wolf;

    public override void Awake(StateController stateController)
    {
        _fov = stateController.GetComponent<FieldOfView>();
        _wolf = stateController.GetComponent<EnemyBehaviour>();
    }

    protected override bool Statement()
    {
        return _fov.TargetFounded();
    }

    public override void OnStateExit()
    {
        if (_fov.visibleTargets.Count > 0)
            _wolf.Target = _fov.visibleTargets[0];
    }

}
