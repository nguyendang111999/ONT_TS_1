using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/IsMoving")]
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
        _playerController = stateController.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        Vector3 movementVector = _playerController.movementInput;
        movementVector.y = 0;
        return movementVector.magnitude > _originSO.minSpeed;
    }
}
