using UnityEngine;
using UnityEngine.Events;

public class OnDescription : MonoBehaviour
{
    private ItemSO item;
    // public event UnityAction UseItemEvent;
    [SerializeField] private UIInventory _uiInventory;
    public ItemSO Item{
        get{
            return item;
        }
        set{
            item = value;
        }
    }
    public void UseItem(){
        // UseItemEvent.Invoke();
        gameObject.SetActive(false);
        _uiInventory.UseItem(item);
    }
}
