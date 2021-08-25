using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Droppable Reward", menuName="Inventory/Reward Drop Rate Config")]
public class DroppableRewardSO : ScriptableObject
{
    [Tooltip("Distance to drop item")]
    [SerializeField]
    private float _distance = default;

    [Tooltip("Items to drop")]
    [SerializeField]
    private DropGroup _dropGroups;

    public float DropDistance => _distance;
    public DropGroup DropGroup => _dropGroups;

    public virtual DropGroup DropSpecialItem(){
        return null;
    }
    
}
