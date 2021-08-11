using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/Falling Action")]
public class FallActionSO : StateActionSO
{
    [SerializeField] private float fallVelocity = 3f;
    public float FallVelocity => fallVelocity;
    protected override StateAction CreateAction() => new FallAction();
}

public class FallAction : StateAction
{
    private PlayerController _thachSanh;

    private float _fallingMovement;
    public override void Awake(StateController stateController)
    {
        _thachSanh = stateController.GetComponent<PlayerController>();
    }

    public override void OnStateEnter(){
        _fallingMovement = _thachSanh.movementInput.y;
    }

    public override void OnStateUpdate()
    {
        _fallingMovement += Physics.gravity.y * PlayerController.GRAVITY * Time.deltaTime;
        Debug.Log("Gravity Y: " + _fallingMovement);
        _thachSanh.movementInput.y = _fallingMovement;
    }
}
