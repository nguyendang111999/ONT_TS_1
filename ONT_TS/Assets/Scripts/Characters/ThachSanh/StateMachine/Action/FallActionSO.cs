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
    private float _fallVelocity;
    private float _fallingMovement;
    private FallActionSO _originSO => (FallActionSO)base.OriginSO;
    public override void Awake(StateController stateController)
    {
        _thachSanh = stateController.GetComponent<PlayerController>();
    }

    public override void OnStateEnter()
    {
        _fallingMovement = 0f;
        _fallVelocity = _originSO.FallVelocity;
    }

    public override void OnStateUpdate()
    {
        if (_fallingMovement > PlayerController.MAX_FALL_SPEED)
            _fallingMovement += Physics.gravity.y * _fallVelocity * Time.deltaTime;
        _thachSanh.movementVector.y += _fallingMovement;
    }
}
