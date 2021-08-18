using System.Collections;
using System.Collections.Generic;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Actions/SpawnAction")]
public class RespawnActionSO : StateActionSO
{
    [SerializeField] private SavePointListSO _list;
    public SavePointListSO SavePointList => _list;
    protected override StateAction CreateAction() => new RespawnAction();
}

public class RespawnAction : StateAction
{
    private Damageable _damageable;

    private SavePointListSO _savePoint;

    private PlayerController _playerController;
    GameObject obj;

    private RespawnActionSO _originSO => (RespawnActionSO)base.OriginSO;

    public override void Awake(StateController stateController)
    {
        _damageable = stateController.GetComponent<Damageable>();
        _playerController = stateController.GetComponent<PlayerController>();
        obj = stateController.gameObject;
        _savePoint = _originSO.SavePointList;
    }

    public override void OnStateEnter()
    {
        Spawn();
    }
    public override void OnStateUpdate() { }

    /// <summary>
    /// Reset health and position of player
    /// </summary>
    public void Spawn()
    {
        float distance = 1f;
        obj.transform.position = _savePoint.GetLastSavePoint() + distance * Vector3.forward;
        Debug.Log("Save pos: " + _savePoint.GetLastSavePoint());
        Debug.Log("Player pos: " + obj.transform.position);
        _damageable.ResetHealth();
    }
}
