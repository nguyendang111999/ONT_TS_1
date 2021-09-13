using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "State Machines/ThachSanh/Condition/Weapon Is Equip Condition")]
public class IsEquipedWeaponSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsEquipedWeapon();
}

public class IsEquipedWeapon : Condition
{
    PlayerController _playerController;

    public override void Awake(StateController stateController)
    {
        _playerController = stateController.GetComponent<PlayerController>();
    }

    protected override bool Statement()
    {
        return _playerController.WeaponEquiped;
    }
}