using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Setting")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] Transform cam;

    [Tooltip("Weapon when Thach Sanh not equiped")]
    [SerializeField] GameObject weaponBack;
    [Tooltip("Weapon when Thach Sanh equiped")]
    [SerializeField] GameObject weaponFront;

    [Header("Sub behaviours")]
    public ObjectPositionSO PlayerPos;
    [SerializeField] private Transform groundDetector;
    public float groundDistance = 0.3f;
    public LayerMask groundLayer;

    private bool isGrounded = true;
    public bool IsGrounded => isGrounded;

    [Header("Movements Setting")]
    public CharacterStatsSO statsSO;
    private Vector2 _inputVector;
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

    private bool onPressEquip = false;
    public bool OnPressEquip => onPressEquip;
    private bool weaponEquiped = true;//Check if player is equipping weapon
    private bool headache = false;

    private const string REBINDS_KEY = "rebinds";

    public bool Headache{
        get{return headache;}
        set{headache = value;}
    }
    public bool WeaponEquiped
    {
        get { return weaponEquiped; }
        set { weaponEquiped = value; }
    }

    #region INPUT ACTION SYSTEM
    private void OnEnable()
    {
        //Resgister movement
        _inputReader.MoveEvent += OnMove;
        _inputReader.StartRunningEvent += OnStartRunning;
        _inputReader.StopRunningEvent += OnStopRunning;

        //Resgister dodge
        _inputReader.DoubleTapDodgeEventPerformed += OnDodgeTrigger;

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
        // _inputReader.HoldHeavyAttackStarted += OnHoldHeavyAttackStart;
        _inputReader.HoldHeavyAttackPerformed += OnHoldHeavyAttackPerform;
        _inputReader.HoldHeavyAttackCanceled += OnHoldHeavyAttackCancel;

        //Register earth ability
        _inputReader.EarthAbilityEvent += EarthPerform;
        _inputReader.EarthAbilityCancelEvent += EarthAbilityCancel;

        //Register life ability
        _inputReader.LifeAbilityEvent += OnLifeAbilityPerform;
        _inputReader.LifeAbilityCancelEvent += OnLifeAbilityCancel;

        //Register equip weapon
        _inputReader.EquipWeaponEvent += OnEquip;

    }
    private void OnDisable()
    {
        //Unresgister movement
        _inputReader.MoveEvent -= OnMove;
        _inputReader.StartRunningEvent -= OnStartRunning;
        _inputReader.StopRunningEvent -= OnStopRunning;

        //Unresgister dodge
        _inputReader.DoubleTapDodgeEventStarted -= OnDodgeTrigger;

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
        // _inputReader.HoldHeavyAttackStarted -= OnHoldHeavyAttackStart;
        _inputReader.HoldHeavyAttackPerformed -= OnHoldHeavyAttackPerform;
        _inputReader.HoldHeavyAttackCanceled -= OnHoldHeavyAttackCancel;

        //Unregister earth ability
        _inputReader.EarthAbilityEvent -= EarthPerform;
        _inputReader.EarthAbilityCancelEvent -= EarthAbilityCancel;

        //Unregister life ability
        _inputReader.LifeAbilityEvent -= OnLifeAbilityPerform;
        _inputReader.LifeAbilityCancelEvent -= OnLifeAbilityCancel;

        //Unregister equip weapon
        _inputReader.EquipWeaponEvent -= OnEquip;

    }
    #endregion

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
        Cursor.visible = false;
        InstantiateMovementData();
        PlayerPos.Transform = transform;
    }

    void Update()
    {
        CheckIfOnGround();
        ReCalculateMovement();
        PlayerPos.Transform = transform;
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

            targetSpeed += targetSpeed * VelocityBoost / 100;
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

    private bool CheckIfOnGround() => isGrounded = Physics.CheckSphere(groundDetector.position, groundDistance, groundLayer);

    //--- Event Listener ---
    private void FalseHeadache(){
        headache = false;
    }
    private void OnMove(Vector2 movement)
    {
        _inputVector = movement.normalized;
    }
    private void OnStartRunning() => isSprinting = true;
    private void OnStopRunning() => isSprinting = false;
    private void OnDodgeTrigger()
    {
        if (!dodgeReady) return;
        else
        {
            DodgeCouroutine = DodgeCountdown(dodgeCounter);
            StartCoroutine(DodgeCouroutine);
            isDashing = true;
            dodgeReady = false;
        }
    }
    public void OnDodgeReset() => isDashing = false; //Used by Animation Event
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
    private void OnJumpCanceled() => isJump = false; //Control by state machine
    private void StopCrouching() => isCrouching = false;
    private void OnAttack() => attackInput = weaponEquiped ? true : false;
    private void OnAttackCanceled() => attackInput = false;//Used by Animation Event
    private void OnTapHeavyAttack() => onHeavyAttack = weaponEquiped ? true : false;
    private void OnTapHeavyAttackCancel() => onHeavyAttack = false;//Used by Animation Event
    // private void OnHoldHeavyAttackStart() => onHoldHeavyAttack = false;
    private void OnHoldHeavyAttackPerform() => onHoldHeavyAttack = true;
    public void OnHoldHeavyAttackCancel() => onHoldHeavyAttack = false;//Used by Animation Event
    private void EarthPerform() => earthPerform = true;//Used by Animation Event
    private void EarthAbilityCancel() => earthPerform = false;
    private void OnLifeAbilityPerform() => lifePerform = true;
    private void OnLifeAbilityCancel() => lifePerform = false;//Used by Animation Event
    public void OnEquipWeapon() //Use by Animation Event
    {
        if (weaponEquiped)
        {
            weaponEquiped = false;
            weaponBack.SetActive(true);
            weaponFront.SetActive(false);
        }
        else
        {
            weaponEquiped = true;
            weaponBack.SetActive(false);
            weaponFront.SetActive(true);
        }
    }
    private void OnEquip()
    {
        if (!equipReady) return;
        EquipCouroutine = EquipCountdown(equipCounter);
        StartCoroutine(EquipCouroutine);
        equipReady = false;
        Debug.Log("PC equiped: " + weaponEquiped);
        if (attackInput || onHeavyAttack || earthPerform) return;
        onPressEquip = true;
    }
    private void OnEquipCancel() => onPressEquip = false;

    private float dodgeCounter = 1.5f;
    bool dodgeReady = true;
    IEnumerator DodgeCouroutine;
    private IEnumerator DodgeCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        dodgeReady = true;
    }

    private float equipCounter = 1.8f;
    bool equipReady = true;
    IEnumerator EquipCouroutine;
    private IEnumerator EquipCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        equipReady = true;
    }
}
