using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decision/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller){
        
        RaycastHit hit;
        
        Debug.DrawRay(controller.attackPoint.position, controller.attackPoint.forward.normalized*controller.stats.lookRange,
        Color.green);

        if(Physics.SphereCast(controller.attackPoint.position, controller.stats.attackRange,
        controller.attackPoint.forward, out hit, controller.stats.lookRange) 
        && hit.collider.CompareTag("Player")){
            controller.chaseTarget = hit.transform;
            controller.currentState.gizmoColor = Color.red;
            return true;
        }

        return false;
    }
}
