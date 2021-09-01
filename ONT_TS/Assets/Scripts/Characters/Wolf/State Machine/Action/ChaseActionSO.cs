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
    private ObjectPositionSO _chaseTarget;
    private WolfStatSO _stat;
    private NavMeshAgent _agent;
    private EnemyBehaviour _wolf;

    public override void Awake(StateController stateController)
    {
        _agent = stateController.GetComponent<NavMeshAgent>();
        _wolf = stateController.GetComponent<EnemyBehaviour>();
        _stat = _wolf.WolfStatSO();
    }

    public override void OnStateEnter()
    {
        _agent.speed = _stat.RunSpeed;
        _chaseTarget = _wolf.Target;
    }

    public override void OnStateUpdate()
    {
        _agent.destination = (_chaseTarget.Transform == null) ? _wolf.SpawnLocation.Location : _chaseTarget.Transform.position;
    }
}
