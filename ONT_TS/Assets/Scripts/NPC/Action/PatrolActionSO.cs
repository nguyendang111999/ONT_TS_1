using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Patrol Action", menuName = "State Machines/Actions/Patrol")]
public class PatrolActionSO : StateActionSO
{
    [SerializeField] private CharStatsSO stats;
    public CharStatsSO CharacterStat => stats;
    protected override StateAction CreateAction() => new PatrolAction();
}
public class PatrolAction : StateAction
{
    private NavMeshAgent _agent;
    private bool _isActiveAgent;
    private PatrolActionSO _origin;
    private Vector3 _startPos;
    private float _patrolRange;
    private CharStatsSO _stats;

    public override void Awake(StateController stateController)
    {
        _agent = stateController.gameObject.GetComponent<NavMeshAgent>();
        _isActiveAgent = _agent != null && _agent.isActiveAndEnabled && _agent.isOnNavMesh;
        _origin = (PatrolActionSO)OriginSO;
        _stats = _origin.CharacterStat;
        _startPos = _stats.startPos;
        _patrolRange = _stats.lookRange;
    }
    public override void OnStateUpdate()
    {
        if (_isActiveAgent && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.isStopped = false;

            float x = Random.Range(_startPos.x - _patrolRange, _startPos.x + _patrolRange);
            float z = Random.Range(_startPos.z - _patrolRange, _startPos.z + _patrolRange);
            _agent.SetDestination(new Vector3(x, _startPos.y, z));
        }
    }

    public override void OnStateEnter()
    {
        if (_isActiveAgent)
        {
            _agent.speed = _stats.patrolSpeed;
            _agent.stoppingDistance = 2f;
        }
    }
}
