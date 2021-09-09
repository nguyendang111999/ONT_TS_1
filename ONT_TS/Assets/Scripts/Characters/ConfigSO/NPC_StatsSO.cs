using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Fox/Fox Stats")]
public class NPC_StatsSO : ScriptableObject
{
    [Tooltip("Range that npc start to notice player")]
    [SerializeField] 
    private float lookRange;
    public float LookRange => lookRange;

    [Tooltip("Range that NPC start to interact with player")]
    [SerializeField] 
    private float reactRange;
    public float ReactRange => reactRange;
    
    [SerializeField] private float walkSpeed;
    public float WalkSpeed => walkSpeed;
    [SerializeField] private float runSpeed;
    public float RunSpeed => runSpeed;
}
