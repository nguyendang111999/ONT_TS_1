using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public CharStats stats;
    public Transform attackPoint;
    public State remainState;
    public Vector3 rootPos;

    [HideInInspector]public NavMeshAgent navMeshAgent;
    [HideInInspector]public List<Transform> wayPointList;
    [HideInInspector]public Transform chaseTarget;

    private bool aiActive;
    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rootPos = gameObject.transform.position;
        navMeshAgent.enabled = true;
    }

    void Update(){
        currentState.UpdateState(this);
    }

    private void OnDrawGizmos() {
        if(currentState != null && attackPoint != null){
            Gizmos.color = currentState.gizmoColor;
            Gizmos.DrawWireSphere(attackPoint.position, stats.attackRange);
        }
    }
    public void TransitionToState(State nextState){
        if(nextState != remainState){
            currentState = nextState;
        }
    }
    
}
