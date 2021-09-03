using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "State Machines/Fox/Actions/Set Next Location")]
public class NPC_NextLocationSO : StateActionSO
{
    protected override StateAction CreateAction() => new NPC_NextLocation();
}

public class NPC_NextLocation : StateAction
{
    private NavMeshAgent _agent;
    private NPCController _controller;
    private PathSO _paths;
    private Vector3 _newDestination;

    public override void Awake(StateController stateController)
    {
        _controller = stateController.GetComponent<NPCController>();
        _agent = stateController.GetComponent<NavMeshAgent>();
        _paths = _controller.Paths;
    }

    public override void OnStateEnter()
    {
        if (!_paths.IfReachedLocation())
        {
            _agent.SetDestination(_paths.GetNextPoint());
        }
    }

    public override void OnStateUpdate()
    { }
}
