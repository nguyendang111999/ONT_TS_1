using UnityEngine;
using UnityEngine.AI;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Chase Action", menuName = "State Machines/Actions/Chase Action")]
public class ChaseActionSO : StateActionSO
{
    private PlayerPositionSO _transform;
    private PlayerPositionSO PlayerPosition => _transform;
    protected override StateAction CreateAction() => new ChaseAction();
}
public class ChaseAction : StateAction
{
    private WolfBehaviour detectPlayer;
    private Transform _chaseTarget;
    private CharacterStatsSO _stat;
    private NavMeshAgent _agent;

    public override void Awake(StateController stateController)
    {
        detectPlayer = stateController.GetComponent<WolfBehaviour>();
        if (detectPlayer != null)
            _stat = detectPlayer.CharStatsSO();
        _agent = stateController.GetComponent<NavMeshAgent>();
    }
    
    public override void OnStateUpdate()
    {
        _chaseTarget = detectPlayer.GetTargetPos();
        _agent.destination = (_chaseTarget == null) ? _stat.StartPosition : _chaseTarget.position;
    }
}
