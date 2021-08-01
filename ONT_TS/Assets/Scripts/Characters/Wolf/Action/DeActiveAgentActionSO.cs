using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "State Machines/Actions/DeActive Agent Action")]
public class DeActiveAgentActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new DeActiveAgentAction();
}

public class DeActiveAgentAction : StateAction
{
    NavMeshAgent _agent;
    public override void Awake(StateController stateController)
    {
        _agent = stateController.GetComponent<NavMeshAgent>();
    }
    public override void OnStateUpdate(){}

    public override void OnStateEnter(){
        _agent.isStopped = true;
    }

}

