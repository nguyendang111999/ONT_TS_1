using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private AbilityContainerSO earthAbilities;
    [SerializeField] private AbilityContainerSO liveAbilities;
    private float _coolDownCounter;
    private AbilityBaseSO[] listAbility;
    private AbilityBaseSO _currentAbility;
    private int earthIndex;
    private int liveIndex;
    [SerializeField] DashAbilitySO dash;

    private void Awake() {
        listAbility = earthAbilities.AbilityList;
        earthIndex = earthAbilities.Index;
        dash.Perform();
    }
    void Update()
    {
        // CoolDownCounter();
    }
    public bool CoolDownCounter(){
        _coolDownCounter = _currentAbility.CoolDownCounter;
        _coolDownCounter = _coolDownCounter >= 0 ? -Time.deltaTime : 0;
        _currentAbility.CoolDownCounter = _coolDownCounter;
        return true;
    }
    public bool IsReady(){
        bool isReady = _coolDownCounter <= 0 ? true : false;
        return true;
    }
}
