using UnityEngine;
using System;

public class AbilityBaseSO : ScriptableObject
{
    [SerializeField] private String _abilityName = default;
    [SerializeField] private Sprite _icon = default;
    [SerializeField] private float _coolDownDuration = default;
    public float CoolDownCounter { get; set; } = 0;

    public String AbilityName => _abilityName;
    public Sprite Icon => _icon;
    public float CoolDownDuration => _coolDownDuration;

    public bool SetCoolDown()
    {
        CoolDownCounter = _coolDownDuration;
        return true;
    }

    public virtual void Perform(){ }
}
