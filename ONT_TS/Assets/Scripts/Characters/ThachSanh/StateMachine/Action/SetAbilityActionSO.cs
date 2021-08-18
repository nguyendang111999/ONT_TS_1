using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/Set ability")]
public class SetAbilityActionSO : StateActionSO
{
    [SerializeField] private AbilityContainerSO _abilities;
    public AbilityType abilityType;
    [Tooltip("This is use to define which ability to use")]
    public string _abilityName;
    public AbilityContainerSO AbilityList => _abilities;
    protected override StateAction CreateAction() => new SetAbility();
    public enum AbilityType
    {
        Melee, Earth, Live,
    }
}
public class SetAbility : StateAction
{
    SetAbilityActionSO _originSO => (SetAbilityActionSO)base.OriginSO;
    private string _abilityName;
    private AbilityContainerSO _abilities;
    private Attack _attack;
    private GameObject _tempObj;

    public override void Awake(StateController stateController)
    {
        _abilities = _originSO.AbilityList;
        _abilityName = _originSO._abilityName;
        _attack = stateController.gameObject.GetComponentInChildren<Attack>(true);
    }

    public override void OnStateEnter()
    {
        GetAbilityDamage();
    }

    public override void OnStateUpdate()
    {}

    public void GetAbilityDamage()
    {
        switch (_originSO.abilityType)
        {
            case SetAbilityActionSO.AbilityType.Melee:
                int index = _abilities.GetAbilityByName(_abilityName);
                _attack.Damage = _abilities.AbilityList[index].Damage;
                break;
            case SetAbilityActionSO.AbilityType.Earth:
                _attack.Damage = _abilities.AbilityList[_abilities.Index].Damage;
                break;
            case SetAbilityActionSO.AbilityType.Live:
                _attack.Damage = _abilities.AbilityList[_abilities.Index].Damage;
                break;
        }
    }
}