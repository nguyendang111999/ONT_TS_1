using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private ObjectPositionSO _target;
    [SerializeField] private WolfStatSO _stat;
    public WolfStatSO WolfStatSO() => _stat;
    public ObjectPositionSO Target
    {
        get { return _target; }
        set { _target = value; }
    }
    private Damageable _damageable;
    public SpawnLocationSO SpawnLocation { get; set; }
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

    private void DeadAction()
    {
        SpawnLocation.TargetKilled += 1;
    }

    public Vector3 StuntLocation()
    {
        if (Target != null)
        {
            Vector3 relativePos1 = new Vector3();
            Vector3 relativePos2 = new Vector3();
            relativePos1.x = (transform.position.x - Target.Transform.position.x);
            relativePos1.z = (transform.position.z - Target.Transform.position.z);
            relativePos1 = relativePos1.normalized;
            relativePos2 = transform.position + relativePos1;
            relativePos2.y = transform.position.y;
            return relativePos2;
        }
        else
            return transform.position;
    }
}
