using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Chase Action", menuName = "State Machines/Actions/Chase Action")]
public class ChaseActionSO : StateActionSO
{
    [SerializeField] private GameObject target;
    public GameObject PlayerPos => target;
    protected override StateAction CreateAction() => new ChaseAction();
}
public class ChaseAction : StateAction
{
    private DetectPlayer detectPlayer;
    private Transform _chaseTarget;
    private CharStatsSO _stat;
    private NavMeshAgent _agent;
    public override void Awake(StateController stateController)
    {
        detectPlayer = stateController.GetComponent<DetectPlayer>();
        if (detectPlayer != null)
            _stat = detectPlayer.CharStatsSO();
        _agent = stateController.GetComponent<NavMeshAgent>();
    }
    public override void OnStateUpdate()
    {
        _chaseTarget = detectPlayer.GetTargetPos();
        _agent.destination = (_chaseTarget == null) ? _stat.startPos : _chaseTarget.position;
    }
}
