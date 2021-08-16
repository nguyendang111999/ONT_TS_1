using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Condition/Is Holding Jump")]
public class IsHoldingJumpSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsHoldingJump();
}

public class IsHoldingJump : Condition
{
    private PlayerController _playerController;

    public override void Awake(StateController stateController){
        _playerController = stateController.GetComponent<PlayerController>();
    }

    protected override bool Statement(){
        return _playerController.IsJump;
    }
}
