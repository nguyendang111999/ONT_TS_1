using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Conditions/Time To Idle")]
public class TimeToIdleSO : StateConditionSO<TimeToIdle> { }

public class TimeToIdle : Condition
{
    float timer = 10f;
    protected override bool Statement()
    {
        timer -= Time.deltaTime;
        return timer < 0;
    }
}
