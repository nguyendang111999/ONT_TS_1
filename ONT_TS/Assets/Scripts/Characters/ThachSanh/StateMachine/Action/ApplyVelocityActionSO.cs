using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AnimatorParameterAction", menuName = "State Machines/ThachSanh/Actions/Update Character Velocity")]
public class ApplyVelocityActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new ApplyVelocityAction();
}

public class ApplyVelocityAction : StateAction
{
    private PlayerController _thachSanh;
    private CharacterController _characterController;
    public override void Awake(StateController stateController)
    {
        _thachSanh = stateController.GetComponent<PlayerController>();
        _characterController = stateController.GetComponent<CharacterController>();
    }
    public override void OnStateUpdate()
    {
        Debug.Log("Movement vector x: " + _thachSanh.movementVector.x);
        Debug.Log("Movement vector z: " + _thachSanh.movementVector.z);
        _characterController.Move(_thachSanh.movementVector * Time.deltaTime);
        _thachSanh.movementVector = _characterController.velocity;
    }
}
