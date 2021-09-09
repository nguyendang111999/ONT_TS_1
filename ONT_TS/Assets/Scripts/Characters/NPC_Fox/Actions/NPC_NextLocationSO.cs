using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "State Machines/Fox/Actions/Set Next Location")]
public class NPC_NextLocationSO : StateActionSO
{
    [SerializeField] private bool isRun = true;
    public bool IsRun => isRun;
    protected override StateAction CreateAction() => new NPC_NextLocation();
}

public class NPC_NextLocation : StateAction
{
    private NavMeshAgent _agent;
    private NPCController _controller;
    private NPC_StatsSO _stats;
    private PathSO _paths;
    private NPC_NextLocationSO _originSO => (NPC_NextLocationSO)base.OriginSO;

    public override void Awake(StateController stateController)
    {
        _controller = stateController.GetComponent<NPCController>();
        _agent = stateController.GetComponent<NavMeshAgent>();
        _paths = _controller.Paths;
        _stats = _controller.Stats;
    }

    public override void OnStateEnter()
    {
        if (_paths.IndexInBound())
        {
            if (_paths.Index == 0)
            {
                _agent.SetDestination(_paths.GetCurrentPoint());
                _paths.Index++;
            }
            else
            {
                _agent.SetDestination(_paths.GetNextPoint());
            }
        }
        _agent.speed = _originSO.IsRun ? _stats.RunSpeed : _stats.WalkSpeed;
    }

    public override void OnStateUpdate()
    { }
}
