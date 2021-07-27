using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using Cinemachine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [Header("Sub behaviours")]

    private HealthBar _healthBar;
    public ObjectPositionSO PlayerPos;
    public Damageable _damageable;

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
    private float timer;
    private float acceleration;
    private float decceleration;
    private float normalRunSpeed;
    private float sprintSpeed;
    private float crouchSpeed;
    private float slideDuration;
    private float slideCountDown = 0f;
    //End: Movement stats

    public bool isCrouching = false;
    private bool isDashing = false;
    public bool IsDashing => isDashing;

    //Manipulate by state machine
    [NonSerialized] public Vector3 movementInput;
    [NonSerialized] public Vector3 movementVector;

    //Attack setting
    [NonSerialized] public bool attackInput = false;
    [NonSerialized] public bool onHeavyAttack = false;
    [NonSerialized] public bool onHoldHeavyAttack = false;

    //INPUT ACTION SYSTEM
    private void OnEnable()
    {
        //Resgister movement
        _inputReader.MoveEvent += OnMove;
        _inputReader.StartRunningEvent += OnStartRunning;
        _inputReader.StopRunningEvent += OnStopRunning;
        //Resgister dodge
        _inputReader.DoubleTapDodgeEventPerformed += OnDashTrigger;

        //Resgister crouch
        // _inputReader.CrouchEvent += OnCrouching;
        // _inputReader.CrouchStopEvent += StopCrouching;
        //Resgister attack
        _inputReader.AttackEvent += OnAttack;
        _inputReader.AttackCanceledEvent += OnAttackCanceled;
        //Resgister heavy attack
        _inputReader.TapHeavyAttackEvent += OnTapHeavyAttack;
        _inputReader.TapHeavyAttackCanceled += OnTapHeavyAttackCancel;
        _inputReader.HoldHeavyAttackStarted += OnHoldHeavyAttackStart;
        _inputReader.HoldHeavyAttackPerformed += OnHoldHeavyAttackPerform;
        _inputReader.HoldHeavyAttackCanceled += OnHoldHeavyAttackCancel;
    }

    private void OnDisable()
    {
        //Unresgister movement
        _inputReader.MoveEvent -= OnMove;
        _inputReader.StartRunningEvent -= OnStartRunning;
        _inputReader.StopRunningEvent -= OnStopRunning;
        //Unresgister dodge
        _inputReader.DoubleTapDodgeEventStarted -= OnDashTrigger;
        //Unresgister crouch
        //_inputReader.CrouchEvent -= OnCrouching;
        //_inputReader.CrouchStopEvent -= StopCrouching;
        //Unresgister attack
        _inputReader.AttackEvent -= OnAttack;
        _inputReader.AttackCanceledEvent -= OnAttackCanceled;
        //Unresgister heavy attack
        _inputReader.TapHeavyAttackEvent -= OnTapHeavyAttack;
        _inputReader.TapHeavyAttackCanceled -= OnTapHeavyAttackCancel;
        _inputReader.HoldHeavyAttackStarted -= OnHoldHeavyAttackStart;
        _inputReader.HoldHeavyAttackPerformed -= OnHoldHeavyAttackPerform;
        _inputReader.HoldHeavyAttackCanceled -= OnHoldHeavyAttackCancel;
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
        PlayerPos.Transform = gameObject.transform;
    }

    void Update()
    {
        ReCalculateMovement();
        PlayerPos.Transform = gameObject.transform;
        Debug.Log("isDashing: " + isDashing);
    }

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
                targetSpeed = 0f;

            if (isCrouching && _velocity >= statsSO.RunSpeed)
            {
                targetSpeed += 10f;
            }

        }
        //Attach velocity
        _velocity = _velocity == targetSpeed ? _velocity = targetSpeed : _velocity < targetSpeed
        ? _velocity += acceleration * Time.deltaTime : _velocity -= decceleration * Time.deltaTime;
        //Round the velocity
        if ((_velocity < targetSpeed && _velocity + acceleration * Time.deltaTime > targetSpeed) ||
            (_velocity > targetSpeed && _velocity - acceleration * Time.deltaTime < targetSpeed))
            _velocity = targetSpeed;

        movementInput = tempDirection.normalized * _velocity;
    }
    void setAim(bool aim)
    {
    }
    void Aim()
    {
        //Aiming with bow
    }
    //--- Event Listener ---
    private void OnMove(Vector2 movement)
    {
        _inputVector = movement.normalized;
    }
    private void OnStartRunning() => isSprinting = true;
    private void OnStopRunning() => isSprinting = false;
    private void OnDashTrigger()
    {
        isDashing = true;
    }
    private void OnDashReset() => isDashing = false;

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
    private void OnTapHeavyAttack() => onHeavyAttack = true;
    private void OnTapHeavyAttackCancel() => onHeavyAttack = false;
    private void OnHoldHeavyAttackStart() => onHoldHeavyAttack = false;
    private void OnHoldHeavyAttackPerform() => onHoldHeavyAttack = true;
    public void OnHoldHeavyAttackCancel() => onHoldHeavyAttack = false;
}
