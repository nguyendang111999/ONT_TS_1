using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Default Action", menuName = "State Machines/Common/Actions/Default Action")]
public class DefaultActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new DefaultAction();
}

public class DefaultAction : StateAction
{
    public override void OnStateUpdate()
    {
        Debug.Log("Default Action");
    }
}