using UnityEngine;
using UnityEngine.AI;

public class WolfBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private ObjectPositionSO _playerPos;
    [SerializeField] private CharacterStatsSO _stat;
    private NavMeshAgent _agent;
    public CharacterStatsSO CharStatsSO() => _stat;
    public ObjectPositionSO PlayerPosition() => _playerPos;
    public Transform AttackPoint() => _attackPoint;
    private Damageable _damageable;
    public SpawnLocationSO Location { get; set; }
    public bool isInActive = false;

    private void OnEnable() {
        _damageable.OnDie += DeadAction;
    }
    private void OnDisable() {
        _damageable.OnDie -= DeadAction;
    }
    private void Awake()
    {
        _damageable = gameObject.GetComponent<Damageable>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void DeadAction()
    {
        Location.TargetKilled += 1;
    }

    private void OnDrawGizmos()
    {
        if (_agent != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_agent.transform.position, _stat.AttackRange);
        }
    }
}
