using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateConditionSO : ScriptableObject
{
    internal StateCondition GetCondition(
        StateController stateController,
        bool expectedResult,
        Dictionary<ScriptableObject, object> createdInstance)
    {
        if (!createdInstance.TryGetValue(this, out var obj))
        {
            var condition = CreateCondition();
            condition._originSO = this;
            createdInstance.Add(this, condition);
            condition.Awake(stateController);

            obj = condition;
        }
        return new StateCondition(stateController, (Condition)obj, expectedResult);
    }
    protected abstract Condition CreateCondition();
}
public abstract class StateConditionSO<T> : StateConditionSO where T : Condition, new()
{
    protected override Condition CreateCondition() => new T();
}

