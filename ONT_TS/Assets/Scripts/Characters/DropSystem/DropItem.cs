using System;
using UnityEngine;

[Serializable]
public class DropItem
{
    [SerializeField]
    private ItemSO _item;

    [SerializeField]
    private float _dropRate;

    public ItemSO Item => _item;
    public float DropRate => _dropRate;
}

