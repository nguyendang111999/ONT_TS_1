using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayerIsInZone", menuName = "State Machines/Conditions/DetectPlayer/Player In Look Range")]
public class PlayerInLookRangeSO : StateConditionSO
{
    protected override Condition CreateCondition() => new PlayerInLookRange();
}

public class PlayerInLookRange : Condition
{
    private PlayerInLookRangeSO _originSO => (PlayerInLookRangeSO)base.OriginSO;
    private ObjectPositionSO _playerPosition;
    private Transform _playerPos;
    private WolfBehaviour _wolf;
    private CharacterStatsSO _stat;
    public override void Awake(StateController stateController){
        _wolf = stateController.GetComponent<WolfBehaviour>();
        _stat = _wolf.CharStatsSO();
        _playerPosition = _wolf.PlayerPosition();
    }
    protected override bool Statement()
    {
        _playerPos = _playerPosition.Transform;
        float distance = Vector3.Distance(_wolf.transform.position, _playerPos.position);
        return distance < _stat.LookRange;
    }
}
