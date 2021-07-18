using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Ability/New Ability Container")]
public class AbilityContainerSO : ScriptableObject
{
    [SerializeField] private int index = default;
    [SerializeField] private AbilityBaseSO[] list;
    public AbilityBaseSO[] AbilityList => list;
    public int Index { get; set; } //Show what ability is being seleced
}
