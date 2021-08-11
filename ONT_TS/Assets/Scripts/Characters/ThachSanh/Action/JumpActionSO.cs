using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

public class JumpActionSO : StateActionSO
{
    [SerializeField] private float jumpForce = 3f;
    public float JumpForce => jumpForce;
    protected override StateAction CreateAction() => new JumpAction();
}
class JumpAction : StateAction
{
    private PlayerController _playerController;

    private float _verticalHeight;

    private float gravity;

    private JumpActionSO _originSO => (JumpActionSO)base.OriginSO;

    public override void Awake(StateController stateController){
        _playerController = stateController.GetComponent<PlayerController>();
    }
    public override void OnStateEnter(){
        _verticalHeight = _originSO.JumpForce;
    }
    public override void OnStateUpdate()
    {
        
    }
}
