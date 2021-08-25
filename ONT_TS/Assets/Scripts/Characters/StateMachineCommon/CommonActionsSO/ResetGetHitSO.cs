using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/Reset Being Hit")]
public class ResetGetHitSO : StateActionSO
{
    protected override StateAction CreateAction() => new ResetGetHit();
}

public class ResetGetHit : StateAction
{
    Damageable _damageable;

    public override void Awake(StateController stateController){
        _damageable = stateController.GetComponent<Damageable>();
    }
    public override void OnStateUpdate(){
        _damageable.GetHit = false;
    }

}
