using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private AbilityContainerSO meleeAbilities;
    [SerializeField] private AbilityContainerSO earthAbilities;
    [SerializeField] private AbilityContainerSO liveAbilities;
    
    public AbilityContainerSO GetMeleeAbilities() => meleeAbilities;
    public AbilityContainerSO GetEarthAbilities() => earthAbilities;
    public AbilityContainerSO GetLiveAbilities() => liveAbilities;

    void Update()
    {
        CoolDownCounter();
    }
    public bool CoolDownCounter(){
        AbilityBaseSO _currentAbility = meleeAbilities.AbilityList[meleeAbilities.Index];
        float _coolDownCounter = _currentAbility.CoolDownCounter;
        _coolDownCounter = _coolDownCounter >= 0 ? -Time.deltaTime : 0;
        _currentAbility.CoolDownCounter = _coolDownCounter;
        return true;
    }
}
