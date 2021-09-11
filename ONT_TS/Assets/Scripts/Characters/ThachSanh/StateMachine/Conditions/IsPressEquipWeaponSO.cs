using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Condition/Is Press Equip")]
public class IsPressEquipWeaponSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsPressEquipWeapon();
}

public class IsPressEquipWeapon : Condition
{
    PlayerController _playerController;

    public override void Awake(StateController stateController){
        _playerController = stateController.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        Debug.Log("Is Press Equip:  " + _playerController.OnPressEquip);
        return _playerController.OnPressEquip;
    }
}
