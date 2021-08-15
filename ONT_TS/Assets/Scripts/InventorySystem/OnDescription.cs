using UnityEngine;
using UnityEngine.Events;

public class OnDescription : MonoBehaviour
{
    private ItemSO item;
    [SerializeField] private UIInventory _uiInventory;
    public ItemSO Item{
        get{
            return item;
        }
        set{
            item = value;
        }
    }

    public void UseItem() //Get call when click use button
    {
        gameObject.SetActive(false);
        _uiInventory.UseItem(item);
    }
}
