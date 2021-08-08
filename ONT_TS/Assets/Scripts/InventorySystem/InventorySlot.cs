using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemSO _items;

    private void OnEnable() {
        DisplayIcon();
    }
    public void DisplayItem()
    {
        if (_items == null)
        {
            Debug.Log("No item to use");
            return;
        }
    }
    public void DisplayIcon()
    {
        Image image;
        GameObject obj = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        if (_items != null)
        {
            image = obj.GetComponent<Image>();
            image.sprite = _items.PreviewImage;
            image.gameObject.SetActive(true);
            Debug.Log("Enabled");
        }

    }
}
