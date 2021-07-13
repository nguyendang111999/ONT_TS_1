using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Is Holding Attack")]
public class IsHoldingAttackSO : StateConditionSO<IsHoldingAttack>{ }
public class IsHoldingAttack : Condition
{
    private PlayerController _playerController;
    public override void Awake(StateController stateController)
    {
        _playerController = stateController.GetComponent<PlayerController>();
    }
    protected override bool Statement()
    {
        return _playerController.attackInput;
    }

}
