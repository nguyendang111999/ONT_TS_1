using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemSO _items;
    [SerializeField] private GameObject panel;
    private GameObject textName;
    private GameObject textDescription;

    private void OnEnable()
    {
        DisplayIcon();
        textName = panel.transform.GetChild(0).gameObject;
        textDescription = panel.transform.GetChild(1).gameObject;
    }
    //Display when click on item. Using by item button.
    public void DisplayItem()
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
