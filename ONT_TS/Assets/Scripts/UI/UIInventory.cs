using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventorySO _currentInventory;
    [SerializeField] private AbilityHolder _abilityHolder;

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
                slots[i]._items = _currentInventory.Items[i];
                slots[i].DisplayIcon();
            }
            else
            {
                slots[i]._items = null;
                slots[i].DisplayIcon();
            }   
        }
    }

    public void UseItem(ItemSO item)
    {
        _abilityHolder.AddUsedItem(item);
        if(item == null){Debug.Log("Shiet");}
        _currentInventory.Remove(item);
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
