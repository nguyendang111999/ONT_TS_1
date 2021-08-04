using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/IsObjectActiveContition")]
public class IsActiveConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsActiveCondition();
}

public class IsActiveCondition : Condition
{
    bool isActive = false;
    Damageable _damageable;
    GameObject _gameObject;
    public override void Awake(StateController stateController){
        _damageable = stateController.GetComponent<Damageable>();
        _gameObject = stateController.gameObject;
    }
    protected override bool Statement() => _gameObject.activeSelf && !_damageable.IsDead;
}
