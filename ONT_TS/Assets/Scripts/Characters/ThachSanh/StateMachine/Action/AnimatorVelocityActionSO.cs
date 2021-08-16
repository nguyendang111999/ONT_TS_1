using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AnimatorParameterAction", menuName = "State Machines/ThachSanh/Actions/Animator Update Velocity")]
public class AnimatorVelocityActionSO : StateActionSO
{
    public string parameterName = default;

    protected override StateAction CreateAction() => new AnimatorVelocityAction(Animator.StringToHash(parameterName));
}
public class AnimatorVelocityAction : StateAction
{
    private Animator _anim;
    private int _parameterHash;
    private float _velocity;
    private PlayerController _thachSanh;
    public AnimatorVelocityAction(int parameterHash)
    {
        _parameterHash = parameterHash;
    }

    public override void Awake(StateController stateController)
    {
        _anim = stateController.GetComponent<Animator>();
        _thachSanh = stateController.GetComponent<PlayerController>();
    }
    public override void OnStateUpdate()
    {
        _velocity = _thachSanh.Velocity;
        _anim.SetFloat(_parameterHash, _velocity);
    }
}
