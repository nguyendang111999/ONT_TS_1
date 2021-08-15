using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemSO _items;
    public ItemSO Item
    {
        get { return _items; }
        set { _items = value; }
    }
    [Tooltip("This is description panel")]
    [SerializeField] private GameObject panel;
    private GameObject textName;
    private GameObject textDescription;

    private void OnEnable()
    {
        DisplayIcon();
        textName = panel.transform.GetChild(0).gameObject;
        textDescription = panel.transform.GetChild(1).gameObject;
    }
    
    public void DisplayItem() //Display when click on item. Using by item button.
    {
        if (_items == null)
        {
            Debug.Log("No item to use");
            return;
        }
        else
        {
            panel.SetActive(true);
            panel.GetComponent<OnDescription>().Item = _items;
            textName.GetComponent<Text>().text = _items.ItemName;
            textDescription.GetComponent<Text>().text = _items.Description;
        }
    }
    public void DisplayIcon()
    {
        Image image;
        GameObject obj = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        if (_items == null)
        {
            image = obj.GetComponent<Image>();
            obj.SetActive(false);
        }
        else
        {
            image = obj.GetComponent<Image>();
            image.sprite = _items.PreviewImage;
            image.gameObject.SetActive(true);
        }
    }
}
