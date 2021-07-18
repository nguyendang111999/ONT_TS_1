using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ONT_TS.StateMachine.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Transition Table", menuName = "State Machines/Transition Table")]
    public class TransitionTableSO : ScriptableObject
    {
        [SerializeField] private TransitionItem[] _transition = default;
        internal State GetIntitialState(StateController stateController)
        {
            var states = new List<State>();
            var transitions = new List<StateTransition>();
            var createdInstance = new Dictionary<ScriptableObject, object>();

            var fromStates = _transition;

            if (fromStates == null)
                throw new ArgumentNullException(fromStates.GetType().Name, $"TransitionTable: {name}");

            foreach (var fromState in fromStates)
            {
                var state = fromState.FromState.GetState(stateController, createdInstance);
                states.Add(state);

                transitions.Clear();

                foreach (var transitionItem in fromState.StateAndCondition)
                {
                    if (transitionItem.ToState == null)
                        throw new ArgumentNullException(nameof(transitionItem.ToState),
                        $"TransitionTable: {name}, From State: {fromState.GetType().Name}");

                    var toState = transitionItem.ToState.GetState(stateController, createdInstance);
                    ProcessConditionUsages(stateController, transitionItem.Conditions, createdInstance,
                    out var conditions, out var resultGroups);
                    transitions.Add(new StateTransition(toState, conditions, resultGroups));
                }
                state._transitions = transitions.ToArray();
            }
            return states.Count > 0 ? states[0]
            : throw new InvalidOperationException();
        }

        private static void ProcessConditionUsages(
            StateController stateController,
            ConditionUsage[] conditionUsages,
            Dictionary<ScriptableObject, object> createdInstance,
            out StateCondition[] conditions,
            out int[] resultGroup)
        {
            int count = conditionUsages.Length;
            conditions = new StateCondition[count];
            //Get each condition
            for (int i = 0; i < count; i++)
            {
                conditions[i] = conditionUsages[i].ConditionSO.GetCondition(
                    stateController, conditionUsages[i].ExpectedResult == Result.True, createdInstance);
            }
            //Get operator and/or
            List<int> resultGroupsList = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int idx = resultGroupsList.Count;
                resultGroupsList.Add(1);
                while (i < count - 1 && conditionUsages[i].Operator == Operator.And)
                {
                    i++;
                    resultGroupsList[idx]++;
                }
            }

            resultGroup = resultGroupsList.ToArray();
        }

        [Serializable]
        public struct TransitionItem
        {
            public StateSO FromState;
            public TransitionTo[] StateAndCondition;
        }
        [Serializable]
        public struct TransitionTo
        {
            public StateSO ToState;
            public ConditionUsage[] Conditions;
        }

        [Serializable]
        public struct ConditionUsage
        {
            public StateConditionSO ConditionSO;
            public Result ExpectedResult;
            public Operator Operator;
        }

        public enum Result { True, False }
        public enum Operator { And, Or }
    }
}