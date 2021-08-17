using System.Collections;
using System.Collections.Generic;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

public class HorizontalMoveActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new HorizontalMoveAction();
}

public class HorizontalMoveAction : StateAction
{
    
    

    public override void OnStateUpdate()
    {
        throw new System.NotImplementedException();
    }
}

