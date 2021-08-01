using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Condition/Ability Is Ready")]
public class IsAbilityReadySO : StateConditionSO
{
    [SerializeField] AbilityContainerSO _abilities;
    public AbilityContainerSO Abilities => _abilities;
    protected override Condition CreateCondition() => new IsAbilityReady();
}
public class IsAbilityReady : Condition
{
    private IsAbilityReadySO _originSO => (IsAbilityReadySO)base.OriginSO;
    private AbilityContainerSO _abilities;
    public override void Awake(StateController stateController){
        _abilities = _originSO.Abilities;
    }

    protected override bool Statement()
    {
        return _abilities.AbilityList[_abilities.Index].IsReady();
    }
}
