using System;
using System.Collections.Generic;
using UnityEngine;

namespace ONT_TS.StateMachine
{
    public class StateController : MonoBehaviour
    {
        [Tooltip("This string is only use for debugging")]
        public String currentState; //Use for debbugging
        [Tooltip("Set character state to this component")]
        [SerializeField] private ScriptableObjects.TransitionTableSO _transitionTable = default;
        internal State _currentState;
        private readonly Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();
        void Awake()
        {
            _currentState = _transitionTable.GetInitialState(this);
        }
#if UNITY_EDITOR
        private void OnEnable()
        {
            UnityEditor.AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
        }
        private void OnAfterAssemblyReload()
        {
            _currentState = _transitionTable.GetInitialState(this);
        }
        private void OnDisable()
        {
            UnityEditor.AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
        }
#endif
        public new bool TryGetComponent<T>(out T component) where T : Component
        {
            var type = typeof(T);
            if (!_cachedComponents.TryGetValue(type, out var value))
            {
                if (base.TryGetComponent<T>(out component))
                    _cachedComponents.Add(type, component);

                return component != null;
            }
            component = (T)value;
            return true;
        }
        public new T GetComponent<T>() where T : Component
        {
            return TryGetComponent(out T component) ?
                component : throw new InvalidOperationException($"{typeof(T).Name} not found in {name}.");
        }
        private void Start()
        {
            _currentState.OnStateEnter();
        }
        void Update()
        {
            if (_currentState.TryGetTransition(out var transitionState))
                Transition(transitionState);

            _currentState.OnStateUpdate();
            //Curren state!
            currentState = gameObject.name + " :" + _currentState._originSO.name;
            
        }

        private void Transition(State transitionState)
        {
            _currentState.OnStateExit();
            _currentState = transitionState;
            _currentState.OnStateEnter();
        }

    }
}