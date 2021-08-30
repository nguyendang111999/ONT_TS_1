using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Wolf/Wolf Stats")]
public class WolfStatSO : ScriptableObject
{
    [SerializeField] private FloatValueSO _attackRange;
    public FloatValueSO AttackRange => _attackRange;

    [SerializeField] private FloatValueSO _lookRange;
    public FloatValueSO LookRange => _lookRange;

    [SerializeField] private float _patrolRange = 15f;
    public float PatrolRange => _patrolRange;

    [SerializeField] private float _walkSpeed = default;
    public float WalkSpeed => _walkSpeed;

    [SerializeField] private float _runSpeed = default;
    public float RunSpeed => _runSpeed;

    private Vector3 _startPos;
    public Vector3 StartPosition => _startPos;
}
