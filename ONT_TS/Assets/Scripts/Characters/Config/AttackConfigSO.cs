using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AttackConfig", menuName = "CharacterConfig/Attack Config")]
public class AttackConfigSO : ScriptableObject
{
    [SerializeField]private int _attackStrength;
    [SerializeField]private float _attackdCooldown;

    public int AttackStrength => _attackStrength;
    public float AttackCoolDown => _attackdCooldown;
}
