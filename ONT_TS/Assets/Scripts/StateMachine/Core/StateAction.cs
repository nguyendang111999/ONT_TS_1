using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAction : IStateComponent
{
    internal StateActionSO _originSO;
    protected StateActionSO OriginSO => _originSO;

    public virtual void Awake(StateController stateController) { }

    public abstract void OnStateUpdate();

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }

    public enum SpecificMoment
    {
        OnStateEnter, OnStateUpdate, OnStateExit,
    }

}
