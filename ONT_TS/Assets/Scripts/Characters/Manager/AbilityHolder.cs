using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private AbilityContainerSO _meleeAbilities;
    [SerializeField] private AbilityContainerSO _earthAbilities;
    [SerializeField] private AbilityContainerSO _lifeAbilities;
    public AbilityContainerSO GetMeleeAbilities() => _meleeAbilities;
    public AbilityContainerSO GetEarthAbilities() => _earthAbilities;
    public AbilityContainerSO GetLiveAbilities() => _lifeAbilities;

    void Update()
    {
        CoolDownCounter(_meleeAbilities);
        CoolDownCounter(_earthAbilities);
        CoolDownCounter(_lifeAbilities);
    }
    public bool CoolDownCounter(AbilityContainerSO container)
    {
        AbilityBaseSO _currentAbility = container.AbilityList[container.Index];
        float _coolDownCounter = _currentAbility.CoolDownCounter;
        _coolDownCounter = _coolDownCounter > 0 ? _coolDownCounter - Time.deltaTime : 0;
        _currentAbility.CoolDownCounter = _coolDownCounter;
        return true;
    }

}
