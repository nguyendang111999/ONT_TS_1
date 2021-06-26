using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Action/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }
    private void Patrol(StateController controller){
        
        if(controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance
        && !controller.navMeshAgent.pathPending){
            Vector3 pos = controller.rootPos;
            float range = controller.stats.lookRange;
            float x = Random.Range(pos.x - range, pos.x + range);
            float z = Random.Range(pos.z - range, pos.z + range);
            controller.navMeshAgent.SetDestination(new Vector3(x, pos.y, z));
        }
    }
}
