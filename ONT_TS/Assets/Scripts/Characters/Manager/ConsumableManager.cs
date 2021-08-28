using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Attack _attack;
    [SerializeField] private Damageable _damageable;
    List<ItemSO> ItemStrengthList = new List<ItemSO>();
    List<ItemSO> ItemSpeedList = new List<ItemSO>();

    void Update()
    {
        AffectCoolDownCounter(ItemStrengthList);
        AffectCoolDownCounter(ItemSpeedList);
        ApplyStrength();
        ApplySpeed();
    }

    public void AddUsedItem(ItemSO item)
    {
        item.ResetCoolDown();
        switch (item.ItemType.ActionType)
        {
            case (ItemActionType.IncreaseStrength):
                ItemStrengthList.Add(item);
                Debug.Log("Add " + ItemStrengthList.Count);
                break;
            case (ItemActionType.IncreaseSpeed):
                ItemSpeedList.Add(item);
                break;
            case (ItemActionType.IncreaseHealth):
                _damageable.Heal(item.BoostNumber);
                break;
        }
    }

    public void AffectCoolDownCounter(List<ItemSO> list)
    {
        if (list.Count < 0) return;
        for (int i = 0; i < list.Count; i++)
        {
            ItemSO item = list[i];
            if (item.CoolDownCounter <= 0)
            {
                list.Remove(item);
                continue;
            }
            item.CoolDownCounter -= Time.deltaTime;
        }
        SortItem(list);
    }

    public void SortItem(List<ItemSO> list)
    {
        if (list.Count <= 0)
        {
            return;
        }
        if (list.Count > 1)
        {
            list.Sort((x, y) => x.BoostNumber.CompareTo(y.BoostNumber));
            list.Reverse();
        }
    }

    public void ApplyStrength()
    {
        _attack.BoostedDamage = ItemStrengthList.Count <= 0 ? 0 : ItemStrengthList[0].BoostNumber;
    }
    public void ApplySpeed() => _playerController.VelocityBoost = ItemSpeedList.Count <= 0 ? 0 : ItemSpeedList[0].BoostNumber;

}
