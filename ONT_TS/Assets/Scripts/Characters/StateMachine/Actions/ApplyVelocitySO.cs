using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
public class ApplyVelocityActionSO : StateActionSO
{
    [SerializeField] private CharacterStatsSO _stats;
    public float WalkSpeed => _stats.WalkSpeed;
    protected override StateAction CreateAction() => new ApplyVelocityAction();
}

public class ApplyVelocityAction : StateAction
{
    private PlayerMovementBehaviour _movementBehaviour;
    private PlayerAnimationBehaviour _animationBehaviour;
    private ApplyVelocityActionSO _originSO => (ApplyVelocityActionSO)base.OriginSO;
    private float _velocity;
    public override void Awake(StateController stateController)
    {
        _movementBehaviour = stateController.GetComponent<PlayerMovementBehaviour>();
        _velocity = _originSO.WalkSpeed;
    }
    public override void OnStateUpdate()
    {
        // _movementBehaviour.
    }
}
