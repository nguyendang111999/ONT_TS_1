using DG.Tweening;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stunt Action", menuName = "State Machines/Wolf/Actions/Stunt Action")]
public class StuntActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new StuntAction();
}
public class StuntAction : StateAction
{
    Vector3 _direction;
    private EnemyBehaviour _wolf;

    public override void Awake(StateController stateController){
        _wolf = stateController.GetComponent<EnemyBehaviour>();
    }

    public override void OnStateEnter()
    {
        _direction = _wolf.StuntLocation();
    }

    public override void OnStateUpdate()
    {
        _wolf.transform.DOMove(_direction, .5f);
    }
}
