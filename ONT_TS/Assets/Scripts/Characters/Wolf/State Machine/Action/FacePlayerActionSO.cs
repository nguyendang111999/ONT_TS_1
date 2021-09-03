 using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Wolf/Actions/Face Player Action")]
public class FacePlayerActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new FacePlayerAction();
}
public class FacePlayerAction : StateAction
{
    Transform _targetPos;
    private EnemyBehaviour _wolf;

    public override void Awake(StateController stateController)
    {
        _wolf = stateController.GetComponent<EnemyBehaviour>();
    }

    public override void OnStateUpdate()
    {
        if(_wolf.Target != null){
            _targetPos = _wolf.Target.Transform;

            Vector3 relativePos = _targetPos.position - _wolf.transform.position;
            relativePos.y = 0;

            Quaternion rotation = Quaternion.LookRotation(relativePos);
            _wolf.transform.rotation = rotation;
        }
    }

}
