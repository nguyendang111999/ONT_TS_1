using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/Gravity On Ground Action")]
public class GravityOnGroundActionSO : StateActionSO
{
    [Tooltip("Vertical force to keep character on ground")]
    [SerializeField] private float verticalPull = -2f;
    public float VerticalPull => verticalPull;
    protected override StateAction CreateAction() => new GravityOnGroundAction();
}

public class GravityOnGroundAction : StateAction
{
    private PlayerController _playerController;

    private GravityOnGroundActionSO _originSO => (GravityOnGroundActionSO)base.OriginSO;

    public override void Awake(StateController stateController){
        _playerController = stateController.GetComponent<PlayerController>();
    }
    
    public override void OnStateUpdate()
    {
        _playerController.movementVector.y = _originSO.VerticalPull;
    }
}
