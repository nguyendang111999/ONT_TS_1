using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Ability/New Ability Container")]
public class AbilityContainerSO : ScriptableObject
{
    [SerializeField] private AbilityBaseSO[] list;
    public AbilityBaseSO[] AbilityList => list;
    public int Index { get; set; } //Show what ability is being seleced
    public int GetAbility(string name){
        for(int i=0; i<list.Length; i++){
            if(list[i].AbilityName.Equals(name)){
                return i;
            }
        }
        return 0;
    }
    public int GetDamage(){
        return list[Index].Damage;
    }
}
