using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/Set ability")]
public class SetMeleeAbilitySO : StateActionSO
{
    public AbilityType abilityType;
    public string _abilityName;
    protected override StateAction CreateAction() => new SetAbility();
    public enum AbilityType{
        Melee, Earth, Live,
    }
}
public class SetAbility : StateAction
{
    SetMeleeAbilitySO _originSO => (SetMeleeAbilitySO)base.OriginSO;
    private string _abilityName;
    private AbilityContainerSO _abilities;
    private AbilityHolder _abilityHolder;

    public override void Awake(StateController stateController)
    {
        _abilityHolder = stateController.GetComponent<AbilityHolder>();
        GetAbility();
        _abilityName = _originSO._abilityName;
    }

    public override void OnStateEnter(){
        _abilities.Index = _abilities.GetAbility(_abilityName);
    }

    public override void OnStateUpdate()
    {
        
    }

    public void GetAbility(){
        switch (_originSO.abilityType)
        {
            case SetMeleeAbilitySO.AbilityType.Melee:
            _abilities = _abilityHolder.GetMeleeAbilities();
            break;
            case SetMeleeAbilitySO.AbilityType.Earth:
            _abilities = _abilityHolder.GetEarthAbilities();
            break;
            case SetMeleeAbilitySO.AbilityType.Live:
            _abilities = _abilityHolder.GetLiveAbilities();
            break;
        }
    }
}