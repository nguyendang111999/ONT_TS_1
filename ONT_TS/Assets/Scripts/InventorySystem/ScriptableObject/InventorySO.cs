using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<ItemSO> _items;
    [SerializeField] private int _slot = default;
    public List<ItemSO> Items {
        get {return _items;}
        set {
            _items = value;
            }
    }
    public void Add(ItemSO item){
        if(_items.Count > _slot) return;
        _items.Add(item);
        _items = _items.OrderBy(item => item.ItemName).ToList();
    }

    public void Remove(ItemSO item){
        if(_items.Count <= 0) return;
        _items.Remove(item);
        _items = _items.OrderBy(item => item.ItemName).ToList();
    }

    public void UseItem(ItemSO item){
        item.UseItem();
        this.Remove(item);
    }
}
