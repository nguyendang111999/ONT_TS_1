using System.Collections;
using System.Collections.Generic;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "New Disable Game Object Action", menuName = "State Machines/Fox/Actions/Disable Game Object")]
public class DisableObjectActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new DisableObjectAction();
}

public class DisableObjectAction : StateAction
{
    private GameObject _gameObject;

    public override void Awake(StateController stateController)
    {
        _gameObject = stateController.gameObject;
    }

    public override void OnStateEnter()
    {
        _gameObject.SetActive(false);
    }

    public override void OnStateUpdate()
    { }
}
