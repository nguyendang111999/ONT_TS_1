using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfig", menuName = "CharacterConfig/Health Config")]
public class HealthConfig : ScriptableObject
{
    [Tooltip("Initial health")]
    [SerializeField] private int _maxHealth;

    public int MaxHealth => _maxHealth;
}
