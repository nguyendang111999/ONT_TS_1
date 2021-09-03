using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Tooltip("Stats of fox")]
    public NPC_StatsSO FoxStats;
    [Tooltip("Player postion")]
    public ObjectPositionSO PlayerPosition;
    [Tooltip("Position of current gameOject")]
    public ObjectPositionSO Position;
    [Tooltip("Path that NPC will lead the player")]
    public PathSO Paths;

    private void Awake() {
        Position.Transform = transform;
    }

    void FixedUpdate()
    {
        Position.Transform = transform;
    }

    public float GetDistanceToPlayer(){
        return PlayerPosition.GetDistance(transform.position);
    }
}
