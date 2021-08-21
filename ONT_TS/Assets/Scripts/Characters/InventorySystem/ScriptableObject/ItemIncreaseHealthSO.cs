using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item/Health")]
public class ItemIncreaseHealthSO : ItemSO
{
    [SerializeField] private int _additionHealth;
    public int AdditionHealth
    {
        get { return _additionHealth; }
    }

}
