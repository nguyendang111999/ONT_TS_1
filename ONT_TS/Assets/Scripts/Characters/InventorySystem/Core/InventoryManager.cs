using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySO _currentInventory;

    void AddItem(ItemSO item){
        _currentInventory.Add(item);
    }
    void RemoveItem(ItemSO item){
        _currentInventory.Remove(item);
    }
    
}
