using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Movement Stats")]
public class CharacterStatsSO : ScriptableObject
{
    public float attackRange = 2f;
    public float lookRange = 15f;
    [Header("Movements info")]
    [SerializeField] private float _acceleration = default;
    [SerializeField] private float _decceleration = default;
    [SerializeField] private float _slideDecceleration = default;

    [SerializeField] private float _walkSpeed = default;
    [SerializeField] private float _runSpeed = default;
    [SerializeField] private float _sprintSpeed = default;
    [SerializeField] private float _crouchSpeed = default;
    [SerializeField] private float _slideDuration = default;
    [SerializeField] private Vector3 _startPos;
    public float Acceleration => _acceleration;
    public float Decceleration => _decceleration;
    public float SlideDecceleration => _slideDecceleration;
    public float RunSpeed => _runSpeed;
    public float SprintSpeed => _sprintSpeed;
    public float CrouchSpeed => _crouchSpeed;
    public float SlideDuration => _slideDuration;
    public float WalkSpeed => _walkSpeed;
    public Vector3 StartPosition => _startPos;
}
