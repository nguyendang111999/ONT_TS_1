using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lore", menuName = "Environment/Lore")]
public class LoreSO : ScriptableObject
{
    [SerializeField] 
    private string loreName;
    public string LoreName => loreName;

    [Tooltip("Image description")]
    [SerializeField] 
    private Sprite icon;
    public Sprite Icon => icon;

    [Tooltip("Detail of a lore")]
    [SerializeField] 
    private string detail;
    public string Detail => detail;

}
