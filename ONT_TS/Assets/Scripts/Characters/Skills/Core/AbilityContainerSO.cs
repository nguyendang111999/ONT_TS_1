using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Ability/New Ability Container")]
public class AbilityContainerSO : ScriptableObject
{
    [Tooltip("Index show that what ability is being seleced")]
    [SerializeField] private int index;
    public List<AbilityBaseSO> _abilityList;
    public List<AbilityBaseSO> AbilityList => _abilityList;
    public int Index
    {
        get { return index; }
        set
        {
            if (value < 0 || value > _abilityList.Count)
                value = 0;
        }
    }
    public int GetAbilityByName(string name)
    {
        for (int i = 0; i < _abilityList.Count; i++)
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
