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

    public override void Awake(StateController stateController){
        _agent = stateController.GetComponent<NavMeshAgent>();
    }

    protected override bool Statement()
    {
        return (_agent.remainingDistance == 0) || (!_agent.hasPath);
    }
}
