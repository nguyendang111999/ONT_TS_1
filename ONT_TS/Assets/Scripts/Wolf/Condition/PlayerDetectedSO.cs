using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

//Use to detect player position

[CreateAssetMenu(fileName = "PlayerIsInZone", menuName = "State Machines/Conditions/PlayerIsInZone")]
public class PlayerDetectedSO : StateConditionSO
{
    private Transform _attackPoint;
    private PlayerPositionSO _transform;
    public Transform AttackPoint => _attackPoint;
    public PlayerPositionSO PlayerPosition => _transform;
    protected override Condition CreateCondition() => new PlayerDetected();
}

public class PlayerDetected : Condition
{
    private PlayerDetectedSO _originSO => (PlayerDetectedSO)base.OriginSO;
    private Transform _attackPoint;
    private WolfBehaviour detectPlayer;
    private Transform _chaseTarget;
    private CharacterStatsSO _stat;
    public override void Awake(StateController stateController)
    {
        detectPlayer = stateController.GetComponent<WolfBehaviour>();
        _stat = detectPlayer?.CharStatsSO();
    }

    protected override bool Statement()
    {
        return DetectPlayerPos();
    }

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
    private void OnDrawGizmos()
    {
        if (_attackPoint != null)
        {
            Gizmos.DrawWireSphere(_attackPoint.position, _stat.attackRange);
        }
    }
}