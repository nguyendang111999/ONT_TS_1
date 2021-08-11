using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Wolf/Conditions/IsInActiveContition")]
public class IsInActiveConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsInActiveCondition();
}
public class IsInActiveCondition : Condition
{
    private EnemyBehaviour _wolf;
    public override void Awake(StateController stateController)
    {
        _wolf = stateController.GetComponent<EnemyBehaviour>();
    }
    protected override bool Statement() => _wolf.isInActive;
}
