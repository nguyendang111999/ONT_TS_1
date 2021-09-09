
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

public class Fox_IfDestinationIsSetSO : StateConditionSO
{
    protected override Condition CreateCondition() => new Fox_IfDestinationIsSet();
}
public class Fox_IfDestinationIsSet : Condition
{

    protected override bool Statement()
    {
        return true;
    }
}
