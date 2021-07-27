using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Dodging Condition")]
public class IsDodgingConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsDodgingCondition();
}
public class IsDodgingCondition : Condition
{
    private bool _isDodging;
    private PlayerController _playerController;

    public override void Awake(StateController stateController){
        _playerController = stateController.GetComponent<PlayerController>();
    }

    protected override bool Statement(){
        _isDodging = _playerController.IsDashing;
        return _isDodging;
    } 
}
