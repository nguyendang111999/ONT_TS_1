using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventorySO _currentInventory;

    private void OnEnable() {
        InventorySlot[] slots = gameObject.GetComponentsInChildren<InventorySlot>();
        FillInventory(slots);
    }

    public void FillInventory(InventorySlot[] slots){
        for (int i = 0; i < _currentInventory.Items.Count; i++)
        {
            slots[i]._items = _currentInventory.Items[i];
            GameObject obj = slots[i].gameObject.transform.GetChild(1).gameObject;
            obj.SetActive(true);
            Debug.Log("Set obj to true");
        }
    }
}
