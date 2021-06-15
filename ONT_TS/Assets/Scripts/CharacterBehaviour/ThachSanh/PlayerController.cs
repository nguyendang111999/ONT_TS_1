using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using Cinemachine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    protected static PlayerController s_Instance;
    public static PlayerController instance { get { return s_Instance; } }

    [Header("Sub behaviours")]
    public PlayerAnimationBehaviour playerAnimationBehaviour;
    public PlayerMovementBehaviour playerMovementBehaviour;

    [Header("Input Setting")]
    protected PlayerInput m_Input;
    private bool sprintPressed = false;
    public CinemachineFreeLook gameCam;
    public CinemachineVirtualCamera aimCam;
    public Rig aimRig;
    public Transform aimTarget;
    Vector2 cameraInput;
    public bool m_isAiming;

    [Header("Movements")]
    public float velocity = 0f;
    [SerializeField] float acceleration = 0.1f;
    [SerializeField] float decceleration = 1f;
    [SerializeField] float slideDecceleration = 1f;
    [SerializeField] float normalRunSpeed = 7f;
    [SerializeField] float sprintSpeed = 10;
    [SerializeField] float slideTime = 1f;
    private float slideCountDown = 0f;
    private Vector3 rawInputMovement;
    private bool isGrounded = true;
    private bool isCrouching = false;
    private bool isMoving = false;

    //Attack setting
    public bool canAttack;
    protected bool m_InAttack;

    public void SetCanAttack(bool canAttack){
        this.canAttack = canAttack;
    }


    //INPUT ACTION SYSTEM
    public void CameraInput(InputAction.CallbackContext value)
    {
        cameraInput = value.ReadValue<Vector2>();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        isMoving = false;
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        if (rawInputMovement.magnitude > 0) isMoving = true;
    }

    public void OnRunning(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            sprintPressed = true;
        }
        if (value.canceled)
        {
            sprintPressed = false;
        }
    }

    public void OnCrouching(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (velocity >= 7f && slideCountDown <= 0f)
            {
                velocity += 5;
                slideCountDown = slideTime;
            }
            isCrouching = true;
        }
        if (value.canceled)
        {
            isCrouching = false;
        }
        playerAnimationBehaviour.PlayCrouchAnimation(isCrouching);
    }

    public void OnJumping(InputAction.CallbackContext value)
    {
        if (value.started && isGrounded)
        {
            playerAnimationBehaviour.ResetTriggerJumpAnimation();
            playerAnimationBehaviour.TriggerJumpAnimation();
            playerMovementBehaviour.Jump();
        }
    }

    public void OnAiming(InputAction.CallbackContext value)
    {
        if (value.performed != m_isAiming)
        {
            setAim(value.performed);
        }
        if (value.performed) Aim();
    }

    // Start is called before the first frame update
    void Awake()
    {
        playerAnimationBehaviour.SetupBehaviour();
    }

    // Update is called once per frame
    void Update()
    {
        CaculateMovementVelocity();
        UpdatePlayerMovement();
        UpdatePlayerMovementAnimation();
        if(m_Input.Attack && canAttack)
        {
            playerAnimationBehaviour.TriggerMeleeAttack();
        }
    }

    void CaculateMovementVelocity()
    {
        float rawInputMagnitude = rawInputMovement.magnitude;

        if (slideCountDown <= 0)
        {
            if (rawInputMagnitude > 0)
            {
                if (velocity <= normalRunSpeed)
                {
                    velocity += Time.deltaTime * acceleration;
                }
                if (sprintPressed && velocity <= sprintSpeed)
                {
                    velocity += Time.deltaTime * acceleration;
                    if (velocity > sprintSpeed) velocity = sprintSpeed;
                }
                if (!sprintPressed && velocity > normalRunSpeed)
                {
                    velocity -= Time.deltaTime * decceleration;
                    if (velocity < normalRunSpeed) velocity = normalRunSpeed;
                }
            }
            if (rawInputMagnitude <= 0f && velocity > 0f)
            {
                velocity -= Time.deltaTime * decceleration;

                if (velocity < 0) velocity = 0f;
            }
        }

        if (slideCountDown > 0f)
        {
            slideCountDown -= Time.deltaTime;
            velocity -= Time.deltaTime * slideDecceleration;
            if (velocity < 0) velocity = 0;
        }
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
        var rot = aimTarget.localRotation.eulerAngles;
        rot.x -= cameraInput.y;
        if (rot.x > 180) rot.x -= 360;
        rot.x = Mathf.Clamp(rot.x, -80, 80);
        aimTarget.localRotation = Quaternion.Slerp(aimTarget.localRotation, Quaternion.Euler(rot), .5f);

        rot = transform.eulerAngles;
        rot.y += cameraInput.x;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot), .5f);
    }
    void UpdatePlayerMovement()
    {
        playerMovementBehaviour.UpdateMovementData(rawInputMovement);
        isGrounded = playerMovementBehaviour.checkIfGrounded();
        playerMovementBehaviour.runSpeed = velocity;
        //Slide movement
        if (slideCountDown > 0)
            playerMovementBehaviour.isSlide(true);
        else playerMovementBehaviour.isSlide(false);
        //If WASD is null
    }

    void UpdatePlayerMovementAnimation()
    {
        playerAnimationBehaviour.UpdateVelocity(velocity);
        playerAnimationBehaviour.SetIsGrounded(isGrounded);

        //Slide movement
        if (slideCountDown > 0)
            playerAnimationBehaviour.SetIsSliding(true);
        else
            playerAnimationBehaviour.SetIsSliding(false);
        //if WASD is null
        playerAnimationBehaviour.SetIsMoving(isMoving);
        Debug.Log("OK!!!");
    }

}
