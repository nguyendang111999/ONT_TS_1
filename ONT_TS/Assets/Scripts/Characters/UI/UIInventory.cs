using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventorySO _currentInventory;
    [SerializeField] private ConsumableManager _consumableManager;

    private void OnEnable()
    {
        InventorySlot[] slots = gameObject.GetComponentsInChildren<InventorySlot>();
        FillInventory(slots);
    }

    public void FillInventory(InventorySlot[] slots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i<_currentInventory.Items.Count)
            {
                slots[i].Item = _currentInventory.Items[i];
                slots[i].DisplayIcon();
            }
            else
            {
                slots[i].Item = null;
                slots[i].DisplayIcon();
            }   
        }
    }

    public void UseItem(ItemSO item)
    {
        _consumableManager.AddUsedItem(item);
        _currentInventory.Remove(item);
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
