using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using Moment = ONT_TS.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "Active NVAgent Action", menuName = "State Machines/Common/Actions/Active NVAgent Action")]
public class DisableNVAgengtActionSO : StateActionSO
{
    [Tooltip("Allow agent to move if false")]
    public bool isStopped = default;
    public Moment WhenToRun = default;
    protected override StateAction CreateAction() => new DisableNVAgengtAction();
}

public class DisableNVAgengtAction : StateAction
{
    private DisableNVAgengtActionSO _originSO => (DisableNVAgengtActionSO)base.OriginSO;
    private NavMeshAgent _agent;

    public override void Awake(StateController stateController)
    {
        _agent = stateController.GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        if(_originSO.WhenToRun == SpecificMoment.OnStateEnter){
            SetAgent();
        }
    }

    public override void OnStateUpdate()
    {
        if(_originSO.WhenToRun == SpecificMoment.OnStateUpdate){
            SetAgent();
        }
    }

    public override void OnStateExit()
    {
        if(_originSO.WhenToRun == SpecificMoment.OnStateExit){
            SetAgent();
        }
    }

    private void SetAgent()
    {
        switch (_originSO.isStopped)
        {
            case true:
                _agent.isStopped = true;
                break;
            case false:
                _agent.isStopped = false;
                break;
        }
    }
}
