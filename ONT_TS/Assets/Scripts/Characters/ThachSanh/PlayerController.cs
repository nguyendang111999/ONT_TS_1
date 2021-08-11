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

    public ObjectPositionSO PlayerPos;

    [Header("Input Setting")]
    [SerializeField] Transform cam;

    [Header("Movements")]
    public CharacterStatsSO statsSO;
    public Vector2 _inputVector;
    public float _velocity = 0f;

    //These variable is use to smooth character rotation
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    public const float GRAVITY = 10f;
    public const float MAX_FALL_SPEED = 20f;

    //Movement stats
    private float acceleration;
    private float decceleration;
    private float normalRunSpeed;
    private float sprintSpeed;
    private float crouchSpeed;
    private float slideDuration;
    private float slideCountDown = 0f;
    //End: Movement stats

    private bool isSprinting = false;
    private bool isCrouching = false;
    public bool IsCrouching => isCrouching;
    private bool isDashing = false;
    public bool IsDashing => isDashing;
    public bool earthPerform = false;
    public bool IsPerformingEarth => earthPerform;
    private bool lifePerform = false;
    private bool IsPerformingLife => lifePerform;

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

        //Register earth ability
        _inputReader.EarthAbilityEvent += EarthPerform;
        _inputReader.EarthAbilityCancelEvent += EarthAbilityCancel;

        //Register life ability
        _inputReader.LifeAbilityEvent += OnLifeAbilityPerform;
        _inputReader.LifeAbilityCancelEvent += OnLifeAbilityCancel;

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

        //Unregister earth ability
        _inputReader.EarthAbilityEvent -= EarthPerform;
        // _inputReader.EarthAbilityCancelEvent -= EarthAbilityCancel;

        //Unregister life ability
        _inputReader.LifeAbilityEvent -= OnLifeAbilityPerform;
        _inputReader.LifeAbilityCancelEvent -= OnLifeAbilityCancel;

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

    void Awake()
    {
        InstantiateMovementData();
        PlayerPos.Transform = gameObject.transform;
    }

    void Update()
    {
        ReCalculateMovement();
        PlayerPos.Transform = gameObject.transform;
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

    //--- Event Listener ---
    private void OnMove(Vector2 movement)
    {
        _inputVector = movement.normalized;
    }
    private void OnStartRunning() => isSprinting = true;
    private void OnStopRunning() => isSprinting = false;
    private void OnDashTrigger() => isDashing = true;
    public void OnDashReset() => isDashing = false; //Used by Animation Event

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
    private void OnAttackCanceled() => attackInput = false;//Used by Animation Event
    private void OnTapHeavyAttack() => onHeavyAttack = true;
    private void OnTapHeavyAttackCancel() => onHeavyAttack = false;//Used by Animation Event
    private void OnHoldHeavyAttackStart() => onHoldHeavyAttack = false;
    private void OnHoldHeavyAttackPerform() => onHoldHeavyAttack = true;
    public void OnHoldHeavyAttackCancel() => onHoldHeavyAttack = false;//Used by Animation Event
    private void EarthAbilityCancel() => earthPerform = false;
    private void EarthPerform() => earthPerform = true;//Used by Animation Event
    private void OnLifeAbilityPerform() => lifePerform = true;
    private void OnLifeAbilityCancel() => lifePerform = false;//Used by Animation Event
}
