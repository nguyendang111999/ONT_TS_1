using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Ability/New Ability Container")]
public class AbilityContainerSO : ScriptableObject
{
    public List<string> a = new List<string>();
    [SerializeField] private AbilityBaseSO[] abilityList;
    public AbilityBaseSO[] AbilityList => abilityList;
    public int Index { get; set; } = 0; //Show what ability is being seleced
    public int GetAbility(string name){
        for(int i=0; i<abilityList.Length; i++){
            if(abilityList[i].AbilityName.Equals(name)){
                return i;
            }
        }
        return 0;
    }
    public int GetDamageByIndex(){
        return abilityList[Index].Damage;
    }
}
