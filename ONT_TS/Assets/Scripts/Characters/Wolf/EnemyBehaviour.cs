using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private WolfStatSO _stat;
    public WolfStatSO WolfStatSO() => _stat;
    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }
    // public Transform AttackPoint() => _attackPoint;
    private Damageable _damageable;
    public SpawnLocationSO Location { get; set; }
    [HideInInspector] public bool isInActive = false;

    private void OnEnable()
    {
        _damageable.OnDie += DeadAction;
    }
    private void OnDisable()
    {
        _damageable.OnDie -= DeadAction;
    }
    private void Awake()
    {
        _damageable = gameObject.GetComponent<Damageable>();
    }

    private void FixedUpdate() {
        Debug.Log("Target is: " + Target.position);
    }

    private void DeadAction()
    {
        Location.TargetKilled += 1;
    }

}
