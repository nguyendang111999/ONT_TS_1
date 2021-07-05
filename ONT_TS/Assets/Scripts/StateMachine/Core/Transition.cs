using UnityEngine;
[System.Serializable]
public class StateTransition : IStateComponent
{
    public State _targetState;
    private StateCondition[] _conditions;
    private int[] _resultGroups;
    private bool[] _results;

    internal StateTransition() { }
    public StateTransition(State targetState, StateCondition[] conditions, int[] resultGroups = null)
    {
        _targetState = targetState;
        _conditions = conditions;
        _resultGroups = resultGroups;
        _results = new bool[_resultGroups.Length];
    }


    public void OnStateEnter()
    {
        for (int i = 0; i < _conditions.Length; i++)
            _conditions[i]._condition.OnStateEnter();
    }

    public void OnStateExit()
    {
        for (int i = 0; i < _conditions.Length; i++)
            _conditions[i]._condition.OnStateExit();
    }

    public bool TryGetTransition(out State state)
    {
        state = ShouldTransition() ? _targetState : null;
        return state != null;
    }
    //Check if condition is met
    private bool ShouldTransition()
    {
        int count = _resultGroups.Length;
        for (int i = 0, idx = 0; i < count && idx < _conditions.Length; i++)
        {
            for (int j = 0; j < _resultGroups[i]; j++, idx++)
            {
                _results[i] = j == 0 ?
                    _conditions[idx].IsMet() :
                    _results[i] && _conditions[idx].IsMet();
            }
        }
        bool ret = false;
        for (int i = 0; i < count && !ret; i++)
            ret = ret || _results[i];

        return ret;
    }

    //Clear cached condition value after transition
    internal void ClearConditionsCache()
    {
        for (int i = 0; i < _conditions.Length; i++)
        {
            _conditions[i]._condition.ClearStatementCache();
        }
    }
}
