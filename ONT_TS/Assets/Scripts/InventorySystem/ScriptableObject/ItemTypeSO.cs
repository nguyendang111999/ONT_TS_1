using UnityEngine;

public enum ItemType{
    Food, Material
}
public enum ItemActionType{
    Eat, Equip, Nothing
}

[CreateAssetMenu(menuName = "Inventory/ItemType")]
public class ItemTypeSO : ScriptableObject
{
    [Tooltip("Name of the action associated with the item type")]
    [SerializeField]
    private string _actionName = default;

    [Tooltip("Item background color in UI")]
    [SerializeField]
    private Color _typeColor = default;

    [Tooltip("Item type")]
    [SerializeField]
    private ItemType _type = default;

    [Tooltip("Item action type")]
    [SerializeField]
    private ItemActionType _actionType = default;

    
    public string ActionName => _actionName;
    public Color TypeColor => _typeColor;
    public ItemType Type => _type;
    public ItemActionType ActionType => _actionType;

}
