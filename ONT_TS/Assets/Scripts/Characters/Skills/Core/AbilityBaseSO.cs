using UnityEngine;
using System;

[CreateAssetMenu(menuName = "CharacterConfig/Ability/Base Ability")]
public class AbilityBaseSO : ScriptableObject
{
    [Tooltip("Parameter to animation you want to play")]
    [SerializeField] private String _abilityName = default;
    [SerializeField] private Sprite _icon = default;
    [SerializeField] private float _coolDownDuration = default;
    [SerializeField] private int _damage = default;

    public float CoolDownCounter { get; set; } = 0;
    
    public String AbilityName => _abilityName;
    public Sprite Icon => _icon;
    public float CoolDownDuration => _coolDownDuration;
    public int Damage => _damage;

    public bool SetCoolDown()
    {
        CoolDownCounter = _coolDownDuration;
        return true;
    }
    public bool IsReady() => CoolDownCounter <= 0;
    public virtual void Perform(){ }
}
