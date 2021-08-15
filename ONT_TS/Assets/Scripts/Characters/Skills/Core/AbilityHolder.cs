using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private AbilityContainerSO _meleeAbilities;
    [SerializeField] private AbilityContainerSO _earthAbilities;
    [SerializeField] private AbilityContainerSO _lifeAbilities;
    public AbilityContainerSO GetMeleeAbilities() => _meleeAbilities;
    public AbilityContainerSO GetEarthAbilities() => _earthAbilities;
    public AbilityContainerSO GetLiveAbilities() => _lifeAbilities;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Attack _attack;
    [SerializeField] private Damageable _damageable;
    List<ItemIncreseStrengthSO> ItemStrengthList = new List<ItemIncreseStrengthSO>();
    List<ItemIncreaseSpeedSO> ItemSpeedList = new List<ItemIncreaseSpeedSO>();

    private float speedBoost;


    void Update()
    {
        CoolDownCounter(_meleeAbilities);
        CoolDownCounter(_earthAbilities);
        CoolDownCounter(_lifeAbilities);
        StrengthItemAffectCoolDown(ItemStrengthList);
        IsBoostingDamage(ItemStrengthList);

    }
    public bool CoolDownCounter(AbilityContainerSO container)
    {
        AbilityBaseSO _currentAbility = container.AbilityList[container.Index];
        float _coolDownCounter = _currentAbility.CoolDownCounter;
        _coolDownCounter = _coolDownCounter > 0 ? _coolDownCounter - Time.deltaTime : 0;
        _currentAbility.CoolDownCounter = _coolDownCounter;
        return true;
    }

    public void AddUsedItem(ItemSO itemIn)
    {
        switch (itemIn.ItemType.ActionType)
        {
            case (ItemActionType.IncreaseStrength):
                if (itemIn == null) { Debug.Log("Shiet"); }
                ItemIncreseStrengthSO itemStrength = (ItemIncreseStrengthSO)itemIn;
                itemStrength.ResetCoolDown();
                ItemStrengthList.Add(itemStrength);
                break;
            case (ItemActionType.IncreaseSpeed):
                ItemIncreaseSpeedSO itemSpeed = (ItemIncreaseSpeedSO)itemIn;
                itemSpeed.ResetCoolDown();
                ItemSpeedList.Add(itemSpeed);
                break;
            case (ItemActionType.IncreaseHealth):
                ItemIncreaseHealthSO item = (ItemIncreaseHealthSO)itemIn;
                _damageable.Heal(item.AdditionHealth);
                Debug.Log("Use Item: " + item.ItemName);
                break;
        }
    }

    public void StrengthItemAffectCoolDown(List<ItemIncreseStrengthSO> list)
    {
        if (list.Count < 0) return;
        for (int i = 0; i < list.Count; i++)
        {
            ItemIncreseStrengthSO item = list[i];
            if (item.CoolDownCounter <= 0)
            {
                list.Remove(item);
                continue;
            }
            ItemIncreseStrengthSO strengthItem = (ItemIncreseStrengthSO)item;
            strengthItem.CoolDownCounter -= Time.deltaTime;
        }
        Debug.Log("List size: " + list.Count);
        // foreach (ItemIncreseStrengthSO item in list)
        // {
        //     if (item.CoolDownCounter <= 0)
        //     {
        //         list.Remove(item);
        //         continue;
        //     }
        //     ItemIncreseStrengthSO strengthItem = (ItemIncreseStrengthSO)item;
        //     strengthItem.CoolDownCounter -= Time.deltaTime;
        // }
    }

    public void IsBoostingDamage(List<ItemIncreseStrengthSO> list)
    {
        Debug.Log("List size: " + list.Count);
        if (list.Count <= 0)
        {
            _attack.BoostedDamage = 0;
            return;
        }
        if (list.Count > 1)
        {
            list.Sort((x, y) => x.AdditionStrength.CompareTo(y.AdditionStrength));
            foreach (ItemIncreseStrengthSO item in list)
            {
                Debug.Log(item.ItemName);
            }
        }
        Debug.Log("addition strength: " + list[0].AdditionStrength);
        _attack.BoostedDamage = list[0].AdditionStrength;
    }
}
