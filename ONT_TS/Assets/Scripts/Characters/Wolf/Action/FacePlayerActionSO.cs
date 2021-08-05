using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/Actions/Face Player Action")]
public class FacePlayerActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new FacePlayerAction();
}
public class FacePlayerAction : StateAction
{
    ObjectPositionSO _playerPos;
    Transform _wolf;

    public override void Awake(StateController stateController)
    {
        _wolf = stateController.transform;
        _playerPos = stateController.GetComponent<EnemyBehaviour>().PlayerPosition();
    }

    public override void OnStateUpdate()
    {
        if(_playerPos.isSet){
            Vector3 relativePos = _playerPos.Transform.position - _wolf.position;
            relativePos.y = 0;

            Quaternion rotation = Quaternion.LookRotation(relativePos);
            _wolf.rotation = rotation;
        }
    }

}
