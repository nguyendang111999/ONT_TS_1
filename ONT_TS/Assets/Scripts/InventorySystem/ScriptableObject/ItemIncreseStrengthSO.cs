using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item/Strength")]
public class ItemIncreseStrengthSO : ItemSO
{
    [SerializeField] private int _additionStrength;
    [SerializeField] private float _coolDown;
    public int AdditionStrength{
        get { return _additionStrength;}
        set { value = _additionStrength;}
    }
    public float CoolDown{
        get { return _coolDown;}
        set { value = _coolDown;}
    }

    public float CoolDownCounter { get; set; }

    public override void ResetCoolDown(){
        CoolDownCounter = _coolDown;
    }
}
