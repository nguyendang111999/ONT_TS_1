using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfig", menuName = "EntityConfig/Health Config")]
public class HealthConfig : ScriptableObject
{
    [Tooltip("Initial health")]
   [SerializeField] private int _maxHealth;
   
   public int MaxHealth => _maxHealth;
}
