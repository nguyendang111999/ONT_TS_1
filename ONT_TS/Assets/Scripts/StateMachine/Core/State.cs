using ONT_TS.StateMachine.ScriptableObjects;

namespace ONT_TS.StateMachine
{
    public class State
    {
        internal StateSO _originSO;
        internal StateController _stateController;
        internal StateAction[] _actions;
        internal StateTransition[] _transitions;

        internal State() { }

        public State(
            StateSO originSO,
            StateController stateController,
            StateTransition[] transitions,
            StateAction[] actions)
        {
            _originSO = originSO;
            _stateController = stateController;
            _transitions = transitions;
            _actions = actions;
        }

        public void OnStateEnter()
        {
            void OnStateEnter(IStateComponent[] comps)
            {
                for (int i = 0; i < comps.Length; i++)
                    comps[i].OnStateEnter();
            }
            OnStateEnter(_transitions);
            OnStateEnter(_actions);
        }
        public void OnStateUpdate()
        {
            for (int i = 0; i < _actions.Length; i++)
                _actions[i].OnStateUpdate();
        }
        public void OnStateExit()
        {
            void OnStateExit(IStateComponent[] comps)
            {
                for (int i = 0; i < comps.Length; i++)
                    comps[i].OnStateExit();
            }
            OnStateExit(_transitions);
            OnStateExit(_actions);
        }
        public bool TryGetTransition(out State state)
        {
            state = null;

            int count = _transitions.Length;

            for (int i = 0; i < count; i++)
                if (_transitions[i].TryGetTransition(out state))
                    break;
            //Clear cached result of each condition
            for (int i = 0; i < count; i++)
                _transitions[i].ClearConditionsCache();

            return state != null;
        }
    }
}