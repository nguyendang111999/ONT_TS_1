using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private ObjectPositionSO _playerPos;
    [SerializeField] private CharacterStatsSO _stat;
    public CharacterStatsSO CharStatsSO() => _stat;
    public ObjectPositionSO PlayerPosition() => _playerPos;
    public Transform AttackPoint() => _attackPoint;
    private Damageable _damageable;
    public SpawnLocationSO Location { get; set; }
    [HideInInspector]public bool isInActive = false;

    private void OnEnable() {
        _damageable.OnDie += DeadAction;
    }
    private void OnDisable() {
        _damageable.OnDie -= DeadAction;
    }
    private void Awake() 
    {
        _damageable = gameObject.GetComponent<Damageable>();
    }

    private void DeadAction()
    {
        Location.TargetKilled += 1;
    }

}
