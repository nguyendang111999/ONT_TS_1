using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AnimatorParameterAction", menuName = "State Machines/Actions/Update Animator Velocity")]
public class AnimatorVelocityActionSO : StateActionSO
{
    public string parameterName = default;

    protected override StateAction CreateAction()=>new AnimatorVelocityAction(Animator.StringToHash(parameterName));
}
public class AnimatorVelocityAction : StateAction
{
    private Animator _anim;
    private int _parameterHash;
    private float _velocity;
    private PlayerController _ThachSanh;
    public AnimatorVelocityAction(int parameterHash){
        _parameterHash = parameterHash;
    }

    public override void Awake(StateController stateController)
    {
        _anim = stateController.GetComponent<Animator>();
        _ThachSanh = stateController.GetComponent<PlayerController>();
    }
    public override void OnStateUpdate()
    {
        _velocity = _ThachSanh._velocity;
        _anim.SetFloat(_parameterHash, _velocity);
    }
}
