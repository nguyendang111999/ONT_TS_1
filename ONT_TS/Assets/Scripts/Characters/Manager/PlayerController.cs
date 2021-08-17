using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input Setting")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] Transform cam;

    [Header("Sub behaviours")]

    public ObjectPositionSO PlayerPos;
    public Transform groundDetector;
    public float groundDistance = 0.3f;
    public LayerMask groundLayer;

    private bool isGrounded = false;
    public bool IsGrounded => isGrounded;

    [Header("Movements Setting")]
    public CharacterStatsSO statsSO;
    public Vector2 _inputVector;
    private float _velocity = 0f;
    public float _velocityDebug;
    public float Velocity => _velocity;

    public float VelocityBoost = 0f; //Speed boosted when using consumable item

    //These variable is use to smooth character rotation
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public const float MAX_FALL_SPEED = -20f;

    //Movement stats
    private float acceleration;
    private float decceleration;
    private float normalRunSpeed;
    private float sprintSpeed;
    private float crouchSpeed;
    private float slideDuration;
    private float slideCountDown = 0f;
    //End: Movement stats

    public bool onVelocityBoost = false;
    private bool isSprinting = false;
    private bool isCrouching = false;
    public bool IsCrouching => isCrouching;
    private bool isJump = false;
    public bool IsJump => isJump;
    private bool isDashing = false;
    public bool IsDashing => isDashing;
    private bool earthPerform = false;
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

        //Register jump
        _inputReader.JumpEvent += OnJump;
        _inputReader.JumpCanceledEvent += OnJumpCanceled;
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

        //Unregister jump
        _inputReader.JumpEvent -= OnJump;
        _inputReader.JumpCanceledEvent -= OnJumpCanceled;

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
        CheckIfGrounded();
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
            transform.rotation = Quaternion.Euler(0, angle, 0);

            //Adjust velocity
            targetSpeed = statsSO.RunSpeed;

            if (isSprinting)
            {
                targetSpeed = statsSO.SprintSpeed;
            }
            if (attackInput || onHeavyAttack)
            {
                targetSpeed = 0f;
            }
            
            if (isCrouching && _velocity >= statsSO.RunSpeed)
            {
                targetSpeed += 10f;
            }
            
            targetSpeed += targetSpeed * VelocityBoost/100;
        }
        //Attach velocity
        _velocity = _velocity == targetSpeed ? _velocity : _velocity < targetSpeed
        ? _velocity += acceleration * Time.deltaTime : _velocity -= decceleration * Time.deltaTime;

        //Round the velocity
        if ((_velocity < targetSpeed && _velocity + acceleration * Time.deltaTime > targetSpeed) ||
            (_velocity > targetSpeed && _velocity - acceleration * Time.deltaTime < targetSpeed))
            _velocity = targetSpeed;

        _velocityDebug = _velocity;
        movementInput = tempDirection.normalized * _velocity;
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundDetector.position, groundDistance, groundLayer);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundDetector.position, groundDistance);
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
    private void OnJump() => isJump = true;
    private void OnJumpCanceled() => isJump = false;
    private void StopCrouching() => isCrouching = false;
    private void OnAttack() => attackInput = true;
    private void OnAttackCanceled() => attackInput = false;//Used by Animation Event
    private void OnTapHeavyAttack() => onHeavyAttack = true;
    private void OnTapHeavyAttackCancel() => onHeavyAttack = false;//Used by Animation Event
    private void OnHoldHeavyAttackStart() => onHoldHeavyAttack = false;
    private void OnHoldHeavyAttackPerform() => onHoldHeavyAttack = true;
    public void OnHoldHeavyAttackCancel() => onHoldHeavyAttack = false;//Used by Animation Event
    private void EarthPerform(){
        earthPerform = true;//Used by Animation Event
        Debug.Log("Earth Ability: " + earthPerform);
    } 
    private void EarthAbilityCancel() => earthPerform = false;
    private void OnLifeAbilityPerform() => lifePerform = true;
    private void OnLifeAbilityCancel() => lifePerform = false;//Used by Animation Event
}
