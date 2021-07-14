using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Actions/SetAnimatorVelocity")]
public class ApplyMovementVelocityActionSO : StateActionSO
{
    public string parameterName;
    protected override StateAction CreateAction() => new ApplyMovementVelocityAction(Animator.StringToHash(parameterName));
}
public class ApplyMovementVelocityAction : StateAction
{
    private Animator _anim;
    private PlayerController _playerController;

    private ApplyMovementVelocityActionSO _originSO => (ApplyMovementVelocityActionSO)base.OriginSO;
    private int _parameterHash;

    public ApplyMovementVelocityAction(int parameterHash)
    {
        _parameterHash = parameterHash;
    }

    public override void Awake(StateController stateController)
    {
        _anim = stateController.GetComponent<Animator>();
        _playerController = stateController.GetComponent<PlayerController>();
    }

    public override void OnStateUpdate()
    {
        float velocity = _playerController.velocity;
        _anim.SetFloat(_parameterHash, velocity);
    }
}
