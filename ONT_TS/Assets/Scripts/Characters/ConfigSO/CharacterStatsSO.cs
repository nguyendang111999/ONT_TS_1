using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Movement Stats")]
public class CharacterStatsSO : ScriptableObject
{
    [SerializeField] private int _exp;
    public int EXP => _exp;
    [SerializeField] private int _health;
    public int Health => _health;
    [SerializeField] private float _stamina;
    public float Stamina => _stamina;

    [Header("Movements info")]
    [SerializeField] private float _acceleration = default;
    public float Acceleration => _acceleration;
    [SerializeField] private float _decceleration = default;
    public float Decceleration => _decceleration;
    [SerializeField] private float _slideDecceleration = default;
    public float SlideDecceleration => _slideDecceleration;
    [SerializeField] private float _walkSpeed = default;
    public float WalkSpeed => _walkSpeed;
    [SerializeField] private float _runSpeed = default;
    public float RunSpeed => _runSpeed;
    [SerializeField] private float _sprintSpeed = default;
    public float SprintSpeed => _sprintSpeed;
    [SerializeField] private float _crouchSpeed = default;
    public float CrouchSpeed => _crouchSpeed;
    [SerializeField] private float _slideDuration = default;
    public float SlideDuration => _slideDuration;

}
