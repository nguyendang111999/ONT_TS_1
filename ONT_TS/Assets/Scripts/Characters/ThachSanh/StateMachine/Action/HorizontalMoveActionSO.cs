using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/Horizontal Movement")]
public class HorizontalMoveActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new HorizontalMoveAction();
}

public class HorizontalMoveAction : StateAction
{
    
    private PlayerController _playerController;

    public override void Awake(StateController stateController){
        _playerController = stateController.GetComponent<PlayerController>();
    }

    public override void OnStateUpdate()
    {
        _playerController.movementVector.x = _playerController.movementInput.x;
        _playerController.movementVector.z = _playerController.movementInput.z;
    }
}

