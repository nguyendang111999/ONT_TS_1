using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/ChaseAction")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chasing(controller);
    }

    private void Chasing(StateController controller)
    {
        Transform chaseTarget = controller.chaseTarget;
        if (chaseTarget != null)
        {
            Vector3 target = chaseTarget.position;
            controller.navMeshAgent.SetDestination(target);
        }
    }
}
