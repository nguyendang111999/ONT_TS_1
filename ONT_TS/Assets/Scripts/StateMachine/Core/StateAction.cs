using ONT_TS.StateMachine.ScriptableObjects;

namespace ONT_TS.StateMachine
{
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
}