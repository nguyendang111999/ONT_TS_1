using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private CharStatsSO _stat;
    private Transform _chaseTarget;

    public CharStatsSO CharStatsSO() => _stat;
    public Transform AttackPoint() => _attackPoint;
    public Transform GetTargetPos() => DetectPlayerPos() ? _chaseTarget : null;

    public bool DetectPlayerPos()
    {
        RaycastHit hit;
        bool result = false;

        Debug.DrawRay(_attackPoint.position, _attackPoint.forward.normalized * _stat.lookRange,
        Color.green);

        if (Physics.SphereCast(_attackPoint.position, _stat.attackRange, _attackPoint.forward, out hit,
        _stat.lookRange) && hit.collider.CompareTag("Player"))
        {
            _chaseTarget = hit.transform;
            result = true;
        }
        return result;
    }
    private void OnDrawGizmos() {
        if(_attackPoint != null){
            Gizmos.DrawWireSphere(_attackPoint.position, _stat.attackRange);
        }
    }
}
