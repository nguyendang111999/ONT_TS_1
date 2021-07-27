
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIsInZone", menuName = "State Machines/Conditions/Timer")]
public class IsWaitingSO : StateConditionSO<IsWaiting>
{
    public float _timer = .5f;
}

public class IsWaiting : Condition
{
    private float _counter;
    private IsWaitingSO _originSO => (IsWaitingSO)base.OriginSO;

    public override void OnStateEnter(){
        _counter = Time.time;
    }

    protected override bool Statement() => Time.time >= _counter + _originSO._timer;
}

