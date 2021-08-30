using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Chase Action", menuName = "State Machines/Wolf/Actions/Chase Action")]
public class ChaseActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new ChaseAction();
}
public class ChaseAction : StateAction
{
    private Transform _chaseTarget;
    private WolfStatSO _stat;
    private FieldOfView _fov;
    private List<Transform> _targets;
    private NavMeshAgent _agent;

    public override void Awake(StateController stateController)
    {
        _agent = stateController.GetComponent<NavMeshAgent>();
        _stat = stateController.GetComponent<EnemyBehaviour>().WolfStatSO();
        _fov = stateController.GetComponent<FieldOfView>();
        _targets = _fov.visibleTargets;
    }

    public override void OnStateEnter()
    {
        if (_targets.Count > 0)
        {
            _chaseTarget = _targets[0];
        }
        _agent.speed = _stat.RunSpeed;
    }
    public override void OnStateUpdate()
    {
        _agent.destination = (_chaseTarget == null) ? _stat.StartPosition : (_chaseTarget.position - Vector3.forward);
    }
}
