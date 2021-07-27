using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Actions/Reset Wolf After Dead")]
public class ResetAfterDeadActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new ResetAfterDeadAction();
}
public class ResetAfterDeadAction : StateAction
{
    private GameObject _gameObject;

    public override void Awake(StateController stateController)
    {
        _gameObject = stateController.gameObject;
    }
    public override void OnStateUpdate() { }
    public override void OnStateExit() => _gameObject.SetActive(false);
}
