using System;
using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using Moment = ONT_TS.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(menuName = "State Machines/Common/Actions/Set Animator Parameter")]
public class AnimatorParameterActionSO : StateActionSO
{
    public ParameterType parameterType = default;
    public String parameterName = default;

    public bool boolValue = default;
    public int intValue = default;
    public float floatValue = default;

    //Allow this action to be resuse on 3 state moment;
    public Moment whenToRun = default;
    protected override StateAction CreateAction() => new AnimatorParameterAction(Animator.StringToHash(parameterName));
    public enum ParameterType
    {
        Bool, Int, Float, Trigger,
    }
}
public class AnimatorParameterAction : StateAction
{
    private Animator _anim;
    private AnimatorParameterActionSO _originSO => (AnimatorParameterActionSO)base.OriginSO;
    private int _parameterHash;

    public AnimatorParameterAction(int parameterHash)
    {
        _parameterHash = parameterHash;
    }

    public override void Awake(StateController stateController)
    {
        _anim = stateController.GetComponent<Animator>();
    }

    public override void OnStateEnter()
    {
        if (_originSO.whenToRun == SpecificMoment.OnStateEnter)
            SetParameter();
    }

    public override void OnStateExit()
    {
        if (_originSO.whenToRun == SpecificMoment.OnStateExit)
            SetParameter();
    }

    public override void OnStateUpdate() { }

    private void SetParameter()
    {
        switch (_originSO.parameterType)
        {
            case AnimatorParameterActionSO.ParameterType.Bool:
                _anim.SetBool(_parameterHash, _originSO.boolValue);
                break;
            case AnimatorParameterActionSO.ParameterType.Float:
                _anim.SetFloat(_parameterHash, _originSO.floatValue);
                break;
            case AnimatorParameterActionSO.ParameterType.Int:
                _anim.SetInteger(_parameterHash, _originSO.intValue);
                break;
            case AnimatorParameterActionSO.ParameterType.Trigger:
                _anim.SetTrigger(_parameterHash);
                break;
        }
    }
}
