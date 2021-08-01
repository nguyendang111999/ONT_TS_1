using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private HealthConfigSO _healthConfig;
    [SerializeField] private HealthBar _healthBar;

    private int _currentHealth = default;
    public bool GetHit { get; set; }
    public bool IsDead { get; set; }
    public int CurrentHealth => _currentHealth;
    public UnityAction OnDie;

    void Awake()
    {
        _currentHealth = _healthConfig.MaxHealth();
        _healthBar.SetMaxHealth(_currentHealth);
    }
    public void ReceiveAttack(int dmg)
    {
        if (IsDead) return;

        _currentHealth -= dmg;
        _healthBar.SetHealth(_currentHealth);

        GetHit = true;

        if (_currentHealth <= 0)
        {
            IsDead = true;
            if (OnDie != null)
            {
                OnDie.Invoke();
            }
        }
    }

    public void ResetHealth()
    {
        _currentHealth = _healthConfig.MaxHealth();
        IsDead = false;
    }
}
