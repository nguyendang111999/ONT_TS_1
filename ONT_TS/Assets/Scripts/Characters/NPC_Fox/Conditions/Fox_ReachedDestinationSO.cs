using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "State Machines/Fox/Conditions/Check If Reached Location")]
public class Fox_ReachedDestinationSO : StateConditionSO
{
    protected override Condition CreateCondition() => new Fox_ReachedDestination();
}

public class Fox_ReachedDestination : Condition
{
    private NavMeshAgent _agent;
    private NPCController _controller;
    private PathSO _paths;

    public override void Awake(StateController stateController){
        _agent = stateController.GetComponent<NavMeshAgent>();
        _controller = stateController.GetComponent<NPCController>();
        _paths = stateController.GetComponent<NPCController>().Paths;
    }

    protected override bool Statement()
    {
        Vector3 pos = _controller.transform.position;
        Vector3 des = _paths.GetCurrentPoint();
        return (pos.x == des.x && pos.z  == des.z) || !_agent.hasPath;
    }
}
