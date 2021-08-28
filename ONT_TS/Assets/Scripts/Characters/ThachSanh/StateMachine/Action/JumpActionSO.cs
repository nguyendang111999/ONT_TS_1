using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/Jump Action")]
public class JumpActionSO : StateActionSO
{
    [SerializeField] private float jumpHeight = 10f;
    public float JumpHeight => jumpHeight;
    protected override StateAction CreateAction() => new JumpAction();
}
class JumpAction : StateAction
{
    private PlayerController _playerController;

    private float _verticalHeight;

    private JumpActionSO _originSO => (JumpActionSO)base.OriginSO;

    public override void Awake(StateController stateController){
        _playerController = stateController.GetComponent<PlayerController>();
        _verticalHeight = _originSO.JumpHeight;
    }
    public override void OnStateEnter(){
        _playerController.movementVector.y = 0;
        _playerController.movementVector.y += Mathf.Sqrt(_verticalHeight * -2f * Physics.gravity.y);
    }
    public override void OnStateUpdate()
    {
    }
}
