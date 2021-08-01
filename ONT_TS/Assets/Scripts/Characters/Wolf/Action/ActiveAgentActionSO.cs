using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "State Machines/Actions/Active Agent Action")]
public class ActiveAgentActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new ActiveAgentAction();
}
public class ActiveAgentAction : StateAction
{
    NavMeshAgent _agent;
    public override void Awake(StateController stateController)
    {
        _agent = stateController.GetComponent<NavMeshAgent>();
    }
    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit(){
        _agent.isStopped = false;
    }
}
