using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/DetectPlayer/Player In Attack Range")]
public class PlayerInAttackRangeSO : StateConditionSO
{
    [SerializeField]private ObjectPositionSO _transform;
    public ObjectPositionSO PlayerPosition => _transform;
    protected override Condition CreateCondition() => new PlayerInAttackRange();
    
}
public class PlayerInAttackRange : Condition
{
    private PlayerInAttackRangeSO _originSO => (PlayerInAttackRangeSO)base.OriginSO;
    private Transform _playerPos;
    private WolfBehaviour _wolf;
    private CharacterStatsSO _wolfStat;

    public override void Awake(StateController stateController){
        _wolf = stateController.GetComponent<WolfBehaviour>();
        _wolfStat = _wolf.CharStatsSO();
    }
    protected override bool Statement()
    {
        _playerPos = _originSO.PlayerPosition.Transform;
        float distance = Vector3.Distance(_wolf.transform.position, _playerPos.position);
        return distance <= _wolfStat.AttackRange;
    }
}
