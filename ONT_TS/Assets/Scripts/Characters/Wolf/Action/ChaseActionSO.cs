using UnityEngine;
using UnityEngine.AI;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Chase Action", menuName = "State Machines/Actions/Chase Action")]
public class ChaseActionSO : StateActionSO
{
    [SerializeField] private ObjectPositionSO _transform;
    public ObjectPositionSO PlayerPosition => _transform;
    protected override StateAction CreateAction() => new ChaseAction();
}
public class ChaseAction : StateAction
{
    private ChaseActionSO _originSO => (ChaseActionSO)base.OriginSO;
    private EnemyBehaviour _wolfBehaviour;
    private Transform _chaseTarget;
    private CharacterStatsSO _stat;
    private NavMeshAgent _agent;

    public override void Awake(StateController stateController)
    {
        _agent = stateController.GetComponent<NavMeshAgent>();
        _stat = stateController.GetComponent<EnemyBehaviour>().CharStatsSO();
    }

    public override void OnStateUpdate()
    {
        _chaseTarget = _originSO.PlayerPosition.Transform;
        _agent.destination = (_chaseTarget == null) ? _stat.StartPosition : _chaseTarget.position;
        _agent.speed = _stat.RunSpeed;
    }
}
