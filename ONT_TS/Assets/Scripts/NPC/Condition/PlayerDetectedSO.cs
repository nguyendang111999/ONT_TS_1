using UnityEngine;
using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;

//Use to detect player position

[CreateAssetMenu(fileName = "PlayerIsInZone", menuName = "State Machines/Conditions/PlayerIsInZone")]
public class PlayerDetectedSO : StateConditionSO
{
    protected override Condition CreateCondition() => new PlayerDetected();
}

public class PlayerDetected : Condition
{
    private DetectPlayer detectPlayer;
    private Transform _chaseTarget;
    private CharStatsSO _stat;
    public override void Awake(StateController stateController)
    {
        detectPlayer = stateController.GetComponent<DetectPlayer>();
        if (detectPlayer != null)
            _stat = detectPlayer.CharStatsSO();
    }

    protected override bool Statement()
    {
        bool result = detectPlayer.DetectPlayerPos();

        return result;
    }
}