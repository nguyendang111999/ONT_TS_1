using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AnimatorParameterAction", menuName = "State Machines/Actions/Update Character Velocity")]
public class ApplyVelocityActionSO : StateActionSO<ApplyVelocityAction> { }

public class ApplyVelocityAction : StateAction
{
    private PlayerController _thachSanh;
    private CharacterController _characterController;
    private Damageable _damageable;
    public override void Awake(StateController stateController)
    {
        _thachSanh = stateController.GetComponent<PlayerController>();
        _characterController = stateController.GetComponent<CharacterController>();
        _damageable = stateController.GetComponent<Damageable>();
    }
    public override void OnStateUpdate()
    {
        _characterController.Move(_thachSanh.movementInput * Time.deltaTime);
        _thachSanh.movementVector = _characterController.velocity;
    }
}
