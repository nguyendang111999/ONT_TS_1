using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using Cinemachine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [Header("Sub behaviours")]
    public PlayerAnimationBehaviour playerAnimationBehaviour;
    public PlayerMovementBehaviour playerMovementBehaviour;

    [Header("Input Setting")]
    private bool isSprinting = false;
    public CinemachineFreeLook gameCam;
    [SerializeField] Transform cam;
    public CinemachineVirtualCamera aimCam;
    public Rig aimRig;
    public Transform aimTarget;
    Vector2 cameraInput;
    public bool m_isAiming;

    [Header("Movements")]
    public CharacterStatsSO statsSO;
    public Vector2 _inputVector;
    public float _velocity = 0f;

    //These variable is use to smooth character rotation
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    //Movement stats
    private float acceleration;
    private float decceleration;
    private float normalRunSpeed;
    private float sprintSpeed;
    private float crouchSpeed;
    private float slideDuration;
    private float slideCountDown = 0f;
    //End: Movement stats

    public bool isCrouching = false;
    private bool isSliding = false;
    [SerializeField] private bool isDodging = false;
    public Vector3 _rawInputMovement;//Movement behaviour

    //Manipulate by state machine
    [NonSerialized] public Vector3 movementInput;
    [NonSerialized] public Vector3 movementVector;

    //Attack setting
    [NonSerialized] public bool attackInput = false;
    [NonSerialized] public bool onHeavyAttack = false;

    //INPUT ACTION SYSTEM
    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.StartRunningEvent += OnStartRunning;
        _inputReader.StopRunningEvent += OnStopRunning;
        _inputReader.DodgeEvent += OnDodgeTrigger;
        _inputReader.CrouchEvent += OnCrouching;
        _inputReader.CrouchStopEvent += StopCrouching;
        _inputReader.AttackEvent += OnAttack;
        _inputReader.AttackCanceledEvent += OnAttackCanceled;
        _inputReader.TapHeavyAttackEvent += OnHeavyAttack;
        _inputReader.HeavyAttackCanceledEvent += OnHeavyAttackCancel;

    }
    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.StartRunningEvent -= OnStartRunning;
        _inputReader.StopRunningEvent -= OnStopRunning;
        _inputReader.DodgeEvent -= OnDodgeTrigger;
        _inputReader.CrouchEvent -= OnCrouching;
        _inputReader.CrouchStopEvent -= StopCrouching;
        _inputReader.AttackEvent -= OnAttack;
        _inputReader.AttackCanceledEvent -= OnAttackCanceled;
        _inputReader.TapHeavyAttackEvent -= OnHeavyAttack;
        _inputReader.HeavyAttackCanceledEvent -= OnHeavyAttackCancel;
    }

    private void InstantiateMovementData()
    {
        acceleration = statsSO.Acceleration;
        decceleration = statsSO.Decceleration;
        normalRunSpeed = statsSO.RunSpeed;
        sprintSpeed = statsSO.SprintSpeed;
        crouchSpeed = statsSO.CrouchSpeed;
        slideDuration = statsSO.SlideDuration;
    }
    public void OnAiming(InputAction.CallbackContext value)
    {
        // if (value.performed != m_isAiming)
        // {
        //     setAim(value.performed);
        // }
        // if (value.performed) Aim();
    }

    void Awake()
    {
        InstantiateMovementData();
        playerAnimationBehaviour.SetupBehaviour();
    }

    void Update()
    {
        ReCalculateMovement();
    }

    // void CaculateMovementVelocity()
    // {
    //     float rawInputMagnitude = _rawInputMovement.magnitude;
    //     isMoving = rawInputMagnitude > 0 ? true : false;
    //     isSliding = (slideCountDown > 0) ? true : false;
    //     slideCountDown = slideCountDown > 0 ? slideCountDown - Time.deltaTime : 0;

    //     if (rawInputMagnitude > 0)
    //     {
    //         if (!isCrouching)
    //         {
    //             if (_velocity <= normalRunSpeed)
    //             {
    //                 _velocity += Time.deltaTime * acceleration;
    //             }
    //             if (isSprinting && _velocity <= sprintSpeed)
    //             {
    //                 _velocity = (_velocity > sprintSpeed && !isSliding) ?
    //                 sprintSpeed : (_velocity + Time.deltaTime * acceleration);
    //             }
    //             if (_velocity > sprintSpeed)
    //             {
    //                 _velocity -= Time.deltaTime * decceleration;
    //             }
    //             if (!isSprinting && _velocity > normalRunSpeed)
    //             {
    //                 _velocity -= Time.deltaTime * decceleration;
    //                 if (_velocity < normalRunSpeed) _velocity = normalRunSpeed;
    //             }
    //         }
    //         if (isCrouching)
    //         {
    //             if (_velocity > crouchSpeed)
    //             {
    //                 _velocity -= Time.deltaTime * decceleration;
    //                 if (_velocity < crouchSpeed) _velocity = crouchSpeed;
    //             }
    //             if (_velocity < crouchSpeed)
    //             {
    //                 _velocity += Time.deltaTime * acceleration;
    //                 if (_velocity > crouchSpeed) _velocity = crouchSpeed;
    //             }
    //         }
    //     }
    //     if (rawInputMagnitude <= 0f && _velocity > 0f)
    //     {
    //         _velocity -= Time.deltaTime * decceleration;
    //         if (_velocity < 0) _velocity = 0f;
    //     }
    // }
    void ReCalculateMovement()
    {
        float targetSpeed = 0f;
        Vector3 tempDirection = new Vector3();

        if (_inputVector.sqrMagnitude == 0f)
        {
            tempDirection = transform.forward * (tempDirection.magnitude + .01f);
        }

        targetSpeed = Mathf.Clamp01(_inputVector.magnitude);
        if (targetSpeed > 0)
        {
            //Calculate character's direction
            float targetAngle = Mathf.Atan2(_inputVector.x, _inputVector.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            tempDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            //Adjust velocity
            targetSpeed = statsSO.RunSpeed;

            if (isSprinting)
                targetSpeed = statsSO.SprintSpeed;

            if (attackInput)
                targetSpeed = 0.5f;

            if(isCrouching && _velocity>= statsSO.RunSpeed){
                targetSpeed += 5f;
            }
        }
        if (targetSpeed < .01f) _velocity = 0;
        targetSpeed = Mathf.Lerp(_velocity, targetSpeed, Time.deltaTime * 4f);

        movementInput = tempDirection.normalized * targetSpeed;

        _velocity = targetSpeed;
        playerAnimationBehaviour.UpdateVelocity(_velocity);
    }
    void setAim(bool aim)
    {
        m_isAiming = aim;
        if (aim)
        {
            transform.rotation = Quaternion.Euler(0f, gameCam.m_XAxis.Value, 0f);
            aimCam.m_Priority = 11;
            DOVirtual.Float(aimRig.weight, 1f, 0.2f, setAimRigWeight);
        }
        else
        {
            aimCam.m_Priority = 1;
            DOVirtual.Float(aimRig.weight, 0, 0.2f, setAimRigWeight);
        }
        void setAimRigWeight(float weight)
        {
            aimRig.weight = weight;
        }
    }
    void Aim()
    {
        //Aiming with bow
    }
    //--- Event Listener ---
    private void OnMove(Vector2 movement)
    {
        _inputVector = movement.normalized;
        _rawInputMovement = new Vector3(_inputVector.x, 0, _inputVector.y);
    }
    private void OnStartRunning() => isSprinting = true;
    private void OnStopRunning() => isSprinting = false;
    private void OnDodgeTrigger() => isDodging = true;
    private void ResetDodgeTrigger() => isDodging = false;
    private void OnCrouching()
    {
        if (_velocity >= 7f && slideCountDown <= 0f)
        {
            _velocity += 5;
            slideCountDown = slideDuration;
        }
        isCrouching = true;
    }
    private void StopCrouching() => isCrouching = false;
    private void OnAttack() => attackInput = true;
    private void OnAttackCanceled() => attackInput = false;//Handle by Animation Event
    private void OnHeavyAttack() => onHeavyAttack = true;
    public void OnHeavyAttackCancel() => onHeavyAttack = false;
}
