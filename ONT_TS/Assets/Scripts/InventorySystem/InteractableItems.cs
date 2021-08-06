using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    [SerializeField] private ItemSO _currentItem = default;
    [SerializeField] private GameObject _go = default;

    public ItemSO GetItem(){
        return _currentItem;
    }

    public void SetItem(ItemSO item){
        _currentItem = item;
    }

}
