using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "PlayerIsInRange", menuName = "State Machines/Wolf/Conditions/Target Is In Range")]
public class PlayerIsInRangeSO : StateConditionSO
{
    [SerializeField] private float _range;
    public float Range => _range;

    protected override Condition CreateCondition() => new PlayerIsInRange();
}

public class PlayerIsInRange : Condition
{
    private PlayerIsInRangeSO _originSO => (PlayerIsInRangeSO)base.OriginSO;
    private EnemyBehaviour _wolf;
    private FieldOfView _fov;
    private float _range;

    public override void Awake(StateController stateController)
    {
        _wolf = stateController.GetComponent<EnemyBehaviour>();
        _fov = stateController.GetComponent<FieldOfView>();
        _range = _originSO.Range;
    }

    protected override bool Statement()
    {
        if(_wolf.Target == null) return false;
        float distance = Vector3.Distance(_wolf.transform.position, _wolf.Target.Transform.position);
        return distance <= _range;
    }
}
