using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item/Speed")]
public class ItemIncreaseSpeedSO : ItemSO
{
    public override void UseItem(){
        Debug.Log("Increase speed");
    }
}
