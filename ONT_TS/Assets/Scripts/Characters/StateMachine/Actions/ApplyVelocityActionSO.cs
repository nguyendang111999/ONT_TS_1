using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AnimatorParameterAction", menuName = "State Machines/Actions/Update Character Velocity")]
public class ApplyVelocityActionSO : StateActionSO<ApplyVelocityAction> { }

public class ApplyVelocityAction : StateAction
{
    private PlayerController _playerController;
    private CharacterController _characterController;
    public override void Awake(StateController stateController)
    {
        _playerController = stateController.GetComponent<PlayerController>();
        _characterController = stateController.GetComponent<CharacterController>();
    }
    public override void OnStateUpdate()
    {
        _characterController.Move(_playerController.movementInput * Time.deltaTime);
        _playerController.movementVector = _characterController.velocity;
        Debug.Log("?");
    }
}
