using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Agent Target Action", menuName = "State Machines/Wolf/Actions/Set Agent Target")]
public class SetTargetActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new SetTargetAction();
}

public class SetTargetAction : StateAction
{
    NavMeshAgent _agent;
    EnemyBehaviour _wolf;
    ObjectPositionSO _target;

    public override void Awake(StateController stateController)
    {
        _agent = stateController.GetComponent<NavMeshAgent>();
        _wolf = stateController.GetComponent<EnemyBehaviour>();
    }

    public override void OnStateEnter()
    {
        if (_wolf.Target != null)
        {
            _target = _wolf.Target;
            _agent.SetDestination(_target.Transform.position);
        }
    }

    public override void OnStateUpdate()
    { }
}
