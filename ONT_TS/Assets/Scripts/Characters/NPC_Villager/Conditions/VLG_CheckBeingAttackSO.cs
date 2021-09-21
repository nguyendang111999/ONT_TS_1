using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "Check Being Attack", menuName = "State Machines/Villager/Conditions/Check Being Attack")]
public class VLG_CheckBeingAttackSO : StateConditionSO
{
    protected override Condition CreateCondition() => new VLG_CheckBeingAttack();
}

public class VLG_CheckBeingAttack : Condition
{
    FieldOfView fieldOfView;
    public override void Awake(StateController stateController)
    {
        fieldOfView = stateController.GetComponent<FieldOfView>();
    }
    protected override bool Statement()
    {
        return fieldOfView.FindEnemy();
    }
}


