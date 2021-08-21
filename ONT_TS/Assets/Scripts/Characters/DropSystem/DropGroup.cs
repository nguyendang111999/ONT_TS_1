using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class DropGroup
{
    [SerializeField]
    private List<DropItem> _drops;

    [SerializeField]
    private float _dropRate;

    public List<DropItem> Drops => _drops;
    public float DropRate => _dropRate;
}
