using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfigSO", menuName = "CharacterConfig/Health Config")]
public class HealthConfigSO : ScriptableObject
{
    [Tooltip("Initial health")]
    [SerializeField] private int _maxHealth;

    public int MaxHealth() => _maxHealth;
}
