using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

public class IsMovingConditionSO : StateConditionSO<IsMovingCondition>
{
    public float minSpeed = 0.01f;
}

public class IsMovingCondition : Condition
{
    private PlayerController _playerController;
    private IsMovingConditionSO _originSO => (IsMovingConditionSO)base.OriginSO;

    public override void Awake(StateController stateController)
    {
        base.Awake(stateController);
    }

    protected override bool Statement()
    {
        return false;
    }
}
