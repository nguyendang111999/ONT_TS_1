using System;
using UnityEngine;

public class AnimatorParameterActionSO : StateActionSO
{
    protected override StateAction CreateAction()
    {
        throw new NotImplementedException();
    }
}
public class AnimatorParameterAction : StateAction
{
    private Animator _animator;
    private AnimatorParameterActionSO _originSO => (AnimatorParameterActionSO)base.OriginSO;
    private int _parameterHash;

    public AnimatorParameterAction(int parameterHash){
        _parameterHash = parameterHash;
    }

    public override void Awake(StateController stateController)
    {
        _animator = stateController.GetComponent<Animator>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnStateUpdate() { }
}
