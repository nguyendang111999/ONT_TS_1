using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Ability/New Ability Container")]
public class AbilityContainerSO : ScriptableObject
{
    [Tooltip("Index show that what ability is being seleced")]
    [SerializeField] private int index;
    public AbilityBaseSO[] _abilityList;
    public AbilityBaseSO[] AbilityList => _abilityList;
    public int Index
    {
        get { return index; }
        set
        {
            if (value < 0 || value > _abilityList.Length)
                value = 0;
        }
    }
    public int GetAbilityByName(string name)
    {
        for (int i = 0; i < _abilityList.Length; i++)
        {
            if (_abilityList[i].AbilityName.Equals(name))
            {
                return i;
            }
        }
        return 0;
    }
    public int GetDamageByIndex()
    {
        return _abilityList[Index].Damage;
    }
}
