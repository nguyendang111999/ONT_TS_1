using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Stats")]
public class CharStatsSO : ScriptableObject
{
    public float patrolSpeed = 2f;
    public float attackRange = 2f;
    public float lookRange = 15f;
    public Vector3 startPos;
}
