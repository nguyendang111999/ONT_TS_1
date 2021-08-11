using UnityEngine;
using UnityEngine.AI;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Patrol Action", menuName = "State Machines/Actions/Patrol")]
public class PatrolActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new PatrolAction();
}
public class PatrolAction : StateAction
{
    private NavMeshAgent _agent;
    private bool _isActiveAgent;
    private EnemyBehaviour _wolfBehaviour;
    private Vector3 _startPos;
    private float _patrolRange;
    private CharacterStatsSO _stats;

    public override void Awake(StateController stateController)
    {
        _agent = stateController.GetComponent<NavMeshAgent>();
        _isActiveAgent = _agent != null && _agent.isActiveAndEnabled && _agent.isOnNavMesh;
        _wolfBehaviour = stateController.GetComponent<EnemyBehaviour>();
        _stats = _wolfBehaviour.CharStatsSO();
        _patrolRange = _stats.LookRange;
    }
    public override void OnStateUpdate()
    {
        if (_isActiveAgent && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            float x = Random.Range(_startPos.x - _patrolRange, _startPos.x + _patrolRange);
            float z = Random.Range(_startPos.z - _patrolRange, _startPos.z + _patrolRange);

            _agent.SetDestination(new Vector3(x, _startPos.y, z));
        }
    }

    public override void OnStateEnter()
    {
        _startPos = _wolfBehaviour.Location.Location;
        _wolfBehaviour.isInActive = false;
        if (_isActiveAgent)
        {
            _agent.speed = _stats.WalkSpeed;
            _agent.stoppingDistance = _stats.AttackRange;
        }
    }
}
