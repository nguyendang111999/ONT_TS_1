using UnityEngine;
using UnityEngine.AI;

public class WolfBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private ObjectPositionSO _playerPos;
    [SerializeField] private CharacterStatsSO _stat;
    private Transform _chaseTarget;
    private NavMeshAgent _agent;
    public CharacterStatsSO CharStatsSO() => _stat;
    public ObjectPositionSO PlayerPosition() => _playerPos;
    public Transform AttackPoint() => _attackPoint;
    // public Transform GetTargetPos() => DetectPlayerPos() ? _chaseTarget : null;

    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
    }
    public bool DetectPlayerPos()
    {
        RaycastHit hit;
        bool result = false;

        Debug.DrawRay(_attackPoint.position, _attackPoint.forward.normalized * _stat.LookRange,
        Color.green);

        if (Physics.SphereCast(_attackPoint.position, _stat.AttackRange, _attackPoint.forward, out hit,
        _stat.LookRange) && hit.collider.CompareTag("Player"))
        {
            _chaseTarget = hit.transform;
            result = true;
        }
        return result;
    }
    private void OnDrawGizmos() {
        if(_agent != null){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_agent.transform.position, _stat.AttackRange);
        }
    }
}
