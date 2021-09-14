using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

//Use to detect player position
[CreateAssetMenu(fileName = "PlayerIsInZone", menuName = "State Machines/Conditions/PlayerIsInZone")]
public class PlayerDetectedSO : StateConditionSO
{
    [SerializeField]private ObjectPositionSO _transform;
    public ObjectPositionSO PlayerPosition() => _transform;
    protected override Condition CreateCondition() => new PlayerDetected();
}

public class PlayerDetected : Condition
{
    private PlayerDetectedSO _originSO => (PlayerDetectedSO)base.OriginSO;
    private Transform _attackPoint;
    private EnemyBehaviour detectPlayer;
    private Transform _chaseTarget;
    private CharacterStatsSO _stat;
    public override void Awake(StateController stateController)
    {
        detectPlayer = stateController.GetComponent<EnemyBehaviour>();
        _stat = detectPlayer?.CharStatsSO();
        _attackPoint = detectPlayer.AttackPoint();
        _chaseTarget = _originSO.PlayerPosition()?.Transform;
    }

    protected override bool Statement()
    {
        return DetectPlayerPos();
    }

    public bool DetectPlayerPos()
    {
        float distance = Vector3.Distance(_attackPoint.position, _chaseTarget.position);
        return distance > _stat.LookRange;
    }
}