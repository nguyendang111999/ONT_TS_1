using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality
{
    Normal, Good, Elite
}

[CreateAssetMenu(fileName="New Item", menuName = "Inventory/Item/Item")]
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

    [Tooltip("Addition number to boost(Strength, Speed, Health)")]
    [SerializeField]
    private int _boostNumber;

    [Tooltip("Time till effect ends")]
    [SerializeField]
    private float _coolDown;

    [Tooltip("Prefab model for item")]
    [SerializeField]
    private GameObject _prefab = default;

    public string ItemName => _itemName;
    public Sprite PreviewImage => _previewImage;
    public ItemTypeSO ItemType => _itemType;
    public string Description => _description;
    public int BoostNumber => _boostNumber;
    public GameObject Prefab => _prefab;
    
    // public float CoolDown
    // {
    //     get { return _coolDown; }
    //     set { value = _coolDown; }
    // }
    public float CoolDownCounter { get; set; }

    public virtual void ResetCoolDown() => CoolDownCounter = _coolDown;
}
