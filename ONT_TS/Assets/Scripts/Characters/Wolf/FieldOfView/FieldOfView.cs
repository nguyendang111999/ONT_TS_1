using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask environmentMask;

    [HideInInspector]
    public List<ObjectPositionSO> visibleTargets = new List<ObjectPositionSO>();

    /// <summary>
    /// Return a vector3 with direction point to the rear of view angle
    /// </summary>
    private void FixedUpdate()
    {
        FindVisibleTarget();
    }

    public Vector3 DirFromAngle(float angleInDegree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegree += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }

    /// <summary>
    /// Find visible target in look range
    /// </summary>
    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) <= viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, environmentMask))
                {
                    if (target.tag.Equals("NPC"))
                    {
                        ObjectPositionSO a = target.GetComponent<NPCController>().Position;
                        visibleTargets.Add(a);
                    }
                    if (target.tag.Equals("Player"))
                    {
                        ObjectPositionSO a = target.GetComponent<PlayerController>().PlayerPos;
                        visibleTargets.Add(a);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Use by villager 2
    /// </summary>
    public bool FindEnemy(){
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        return targetInViewRadius.Length > 0;
    }

    public bool TargetFounded() => visibleTargets.Count > 0;

}
