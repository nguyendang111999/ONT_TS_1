using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : IStateComponent
{
    private bool _isCached = false;
    private bool _cachedStatement = default;
    internal StateConditionSO _originSO;

    //Get access to StateConsitionSO
    protected StateConditionSO OriginSO => _originSO;

    //Get the result statement to evaluate
    protected abstract bool Statement();

    //Cache the statement
    internal bool GetStatement()
    {
        if (!_isCached)
        {
            _isCached = true;
            _cachedStatement = Statement();
        }
        return true;
    }
    internal void ClearStatementCache()
    {
        _isCached = false;
    }

    //Use this method to get Component from main object
    public virtual void Awake(StateController stateController) { }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

}

//This contain condition and expected result
public readonly struct StateCondition
{
    internal readonly StateController _stateController;
    internal readonly Condition _condition;
    internal readonly bool _expectedResult;

    public StateCondition(StateController stateController, Condition condition, bool expectedResult)
    {
        _stateController = stateController;
        _condition = condition;
        _expectedResult = expectedResult;
    }

    public bool IsMet()
    {
        bool statement = _condition.GetStatement();
        bool isMet = statement == _expectedResult;
        return isMet;
    }
}
