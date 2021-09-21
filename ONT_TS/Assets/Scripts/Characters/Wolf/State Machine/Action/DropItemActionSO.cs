using ONT_TS.StateMachine;
using ONT_TS.StateMachine.ScriptableObjects;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Chase Action", menuName = "State Machines/Wolf/Actions/Drop Item Action")]
public class DropItemActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new DropItemAction();
}

public class DropItemAction : StateAction
{
    private DroppableRewardSO _dropReward;
    private Transform _currenTransform;

    public override void Awake(StateController stateController)
    {
        _dropReward = stateController.GetComponent<Damageable>().DroppableRewardConfig;
        _currenTransform = stateController.transform;
    }

    public override void OnStateEnter()
    {
        DropRewards(_currenTransform.position);
    }

    public override void OnStateUpdate() { }

    private void DropRewards(Vector3 pos)
    {
        DropGroup dropGroup = _dropReward.DropGroup;
        float randValue = Random.value;

        if (dropGroup.DropRate >= randValue)
        {
            DropOneReward(dropGroup, pos);
        }
    }

    private void DropOneReward(DropGroup dropGroup, Vector3 pos)
    {
        float dropDice = Random.value;
        float currentRate = 0f;

        ItemSO item = null;
        GameObject itemPrefab = null;

        foreach (DropItem dropItem in dropGroup.Drops)
        {
            currentRate += dropItem.DropRate;
            if (currentRate >= dropDice)
            {
                item = dropItem.Item;
                itemPrefab = dropItem.Item.Prefab;
                break;
            }
        }
        Debug.Log("Item: " + item.ItemName);
        float randAngle = Random.value * Mathf.PI * 2;
        GameObject collectibleItem = GameObject.Instantiate(itemPrefab, pos + _dropReward.DropDistance * Vector3.forward, Quaternion.identity);
    }
}
