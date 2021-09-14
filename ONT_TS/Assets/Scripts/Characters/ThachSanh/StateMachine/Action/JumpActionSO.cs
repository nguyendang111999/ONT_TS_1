using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/Jump Action")]
public class JumpActionSO : StateActionSO
{
    [SerializeField] private float jumpForce = 10f;
    public float JumpForce => jumpForce;
    protected override StateAction CreateAction() => new JumpAction();
}
class JumpAction : StateAction
{
    private PlayerController _playerController;

    private float _verticalHeight;

    private float jumpForce;

    private JumpActionSO _originSO => (JumpActionSO)base.OriginSO;

    public override void Awake(StateController stateController){
        _playerController = stateController.GetComponent<PlayerController>();
        jumpForce = _originSO.JumpForce;
    }
    public override void OnStateEnter(){
        _verticalHeight = _originSO.JumpForce;
        _playerController.movementVector.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y) ;
    }
    public override void OnStateUpdate()
    {
        // _playerController.movementInput.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
    }
}
