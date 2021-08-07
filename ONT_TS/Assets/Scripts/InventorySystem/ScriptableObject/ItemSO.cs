using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality{
    Normal, Good, Elite
}

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    [Tooltip("Name of the item")]
    [SerializeField]
    private string _itemName = default;

    [Tooltip("Image for item")]
    [SerializeField]
    private Sprite _previewImage = default;

    [Tooltip("Type of item")]
    [SerializeField]
    private ItemTypeSO _itemType = default;

    [Tooltip("Description for item")]
    [SerializeField]
    private string _description = default;

    [Tooltip("Prefab model for item")]
    [SerializeField]
    private GameObject _prefab = default;

    public string ItemName => _itemName;
    public Sprite PreviewImage => _previewImage;
    public ItemTypeSO ItemType => _itemType;
    public string Description => _description;
    public GameObject Prefab => _prefab;

    public virtual void UseItem(){}
}
