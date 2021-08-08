using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item/Health")]
public class ItemIncreaseHealthSO : ItemSO
{
    public override void UseItem(){
        Debug.Log("Increase health");
    }
}
