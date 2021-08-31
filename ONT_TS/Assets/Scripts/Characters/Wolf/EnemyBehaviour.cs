using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // [SerializeField] private Vector3 _startPos;
    // public Vector3 StartPos => _startPos;
    [SerializeField] private ObjectPositionSO _target;
    [SerializeField] private WolfStatSO _stat;
    public WolfStatSO WolfStatSO() => _stat;
    public ObjectPositionSO Target
    {
        get { return _target; }
        set { _target = value; }
    }
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

    private void FixedUpdate()
    {
        if (Target != null)
            Debug.Log("Target is: " + Target.Transform.position);
    }

    private void DeadAction()
    {
        Location.TargetKilled += 1;
    }

}
