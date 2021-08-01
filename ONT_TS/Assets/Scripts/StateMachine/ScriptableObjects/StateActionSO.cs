using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ONT_TS.StateMachine.ScriptableObjects
{
    public abstract class StateActionSO : ScriptableObject
    {
        //Create new StateAction or return existing one in createdInstance
        internal StateAction GetAction(StateController stateController, Dictionary<ScriptableObject, object> createdInstance)
        {
            if (createdInstance.TryGetValue(this, out var obj))
                return (StateAction)obj;
            var action = CreateAction();
            createdInstance.Add(this, action);
            action._originSO = this;
            action.Awake(stateController);
            return action;
        }
        protected abstract StateAction CreateAction();
    }
    public abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
    {
        protected override StateAction CreateAction() => new T();
    }
}
