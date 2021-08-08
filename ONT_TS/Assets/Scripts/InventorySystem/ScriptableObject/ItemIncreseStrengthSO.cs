using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item/Strength")]
public class ItemIncreseStrengthSO : ItemSO
{
    public override void UseItem(){
        Debug.Log("Increase Strength");
    }
}
