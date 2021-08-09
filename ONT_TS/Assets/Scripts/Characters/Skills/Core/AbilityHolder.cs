using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private AbilityContainerSO _meleeAbilities;
    [SerializeField] private AbilityContainerSO _earthAbilities;
    [SerializeField] private AbilityContainerSO _lifeAbilities;
    [SerializeField] private Attack _attack;
    List<ItemSO> ItemStrengthList = new List<ItemSO>();
    List<ItemSO> ItemSpeedList = new List<ItemSO>();

    public AbilityContainerSO GetMeleeAbilities() => _meleeAbilities;
    public AbilityContainerSO GetEarthAbilities() => _earthAbilities;
    public AbilityContainerSO GetLiveAbilities() => _lifeAbilities;

    [SerializeField] private Damageable _damageable;
    
    void Update()
    {
        CoolDownCounter(_meleeAbilities);
        CoolDownCounter(_earthAbilities);
        CoolDownCounter(_lifeAbilities);
    }
    public bool CoolDownCounter(AbilityContainerSO container)
    {
        AbilityBaseSO _currentAbility = container.AbilityList[container.Index];
        float _coolDownCounter = _currentAbility.CoolDownCounter;
        _coolDownCounter = _coolDownCounter > 0 ? _coolDownCounter - Time.deltaTime : 0;
        _currentAbility.CoolDownCounter = _coolDownCounter;
        return true;
    }

    public void AddUsedItem(ItemSO itemIn){
        switch(itemIn.ItemType.ActionType){
            case(ItemActionType.IncreaseStrength):

            break;
            case(ItemActionType.IncreaseSpeed):
                
            break;
            case(ItemActionType.IncreaseHealth):
                ItemIncreaseHealthSO item = (ItemIncreaseHealthSO) itemIn;
                _damageable.Heal(item.AdditionHealth);
                Debug.Log("Use Item: " + item.ItemName);
            break;
        }
    }

    public void ItemAffectCoolDown(){
        foreach(var item in ItemStrengthList){
            if(item.GetType().Equals(typeof(ItemIncreseStrengthSO))){
                ItemIncreseStrengthSO strengthItem = (ItemIncreseStrengthSO)item;
                strengthItem.CoolDownCounter -= Time.deltaTime;
            }
        }
    }
    public bool IsBoostingDamage(){
        if(ItemStrengthList.Count <= 0) return false;
        return true;
    }
}
