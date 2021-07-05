using UnityEngine;

public enum ZoneType{
    Alert,
    Attack
}

//Use to detect player position

[CreateAssetMenu(fileName = "PlayerIsInZone", menuName = "State Machines/Conditions/PlayerIsInZone")]
public class PlayerIsInZoneSO : StateConditionSO
{
    public ZoneType zone;
    protected override Condition CreateCondition() => new PlayerIsInZone();
}

public class PlayerIsInZone : Condition
{
    private GameObject target;
    private Transform _pos;

    public override void Awake(StateController stateController)
    {
        _pos = stateController.GetComponent<Transform>();
    }
    protected override bool Statement()
    {
        bool result = true;
        return result;
    }
}

