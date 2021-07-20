using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private HealthConfig _healthConfig;

    private int _currentHealth = default;
    public bool GetHit { get; set; }
    public bool isDead { get; set; }
    public int CurrentHealth => _currentHealth;
    public UnityAction OnDie;

    private void Awake()
    {
        _currentHealth = _healthConfig.MaxHealth;
        // Debug.Log(gameObject.name + " health: " + _currentHealth);
    }
    public void ReceiveAttack(int dmg)
    {
        if (isDead) return;

        _currentHealth -= dmg;
        // GetHit = true;
        if (_currentHealth <= 0)
        {
            isDead = true;
            if (OnDie != null)
            {
                OnDie.Invoke();
            }
        }
    }

    public void ResetHealth()
    {
        _currentHealth = _healthConfig.MaxHealth;
        isDead = false;
    }
}
