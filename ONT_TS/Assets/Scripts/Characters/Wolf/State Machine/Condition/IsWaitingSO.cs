
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "New Time Counter", menuName = "State Machines/Common/Conditions/New Timer")]
public class IsWaitingSO : StateConditionSO
{
    [Tooltip("After the certain time you've declare, this condition will return true")]
    public float _timer = .5f;
    protected override Condition CreateCondition() => new IsWaiting();
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

