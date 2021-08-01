using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private AbilityContainerSO meleeAbilities;
    [SerializeField] private AbilityContainerSO earthAbilities;
    [SerializeField] private AbilityContainerSO lifeAbilities;

    public AbilityContainerSO GetMeleeAbilities() => meleeAbilities;
    public AbilityContainerSO GetEarthAbilities() => earthAbilities;
    public AbilityContainerSO GetLiveAbilities() => lifeAbilities;

    void Update()
    {
        CoolDownCounter(meleeAbilities);
        CoolDownCounter(earthAbilities);
        CoolDownCounter(lifeAbilities);
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
