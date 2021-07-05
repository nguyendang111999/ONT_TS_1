using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "State Machines/State")]
public class StateSO : ScriptableObject
{
    [SerializeField] private StateActionSO[] _actions = null;
    
    //Get state
    internal State GetState(StateController stateController, Dictionary<ScriptableObject, object> createdInstance){
        if(createdInstance.TryGetValue(this, out var obj)){
            return (State)obj;
        }
        var state = new State();
        createdInstance.Add(this, state);
        state._originSO = this;
        state._stateController = stateController;
        state._transitions = new StateTransition[0];
        state._actions = GetActions(_actions, stateController, createdInstance);
        
        return state;
    }

    //Get all state actions
    private static StateAction[] GetActions(StateActionSO[] scriptableActions,
    StateController stateController, Dictionary<ScriptableObject, object> createdInstance){
        int count = scriptableActions.Length;
        var actions = new StateAction[count];
        for (int i = 0; i < count; i++)
        {
            actions[i]=scriptableActions[i].GetAction(stateController, createdInstance);
        }
        return actions;
    }
}
