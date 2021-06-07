using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Sub behaviours")]
    public PlayerAnimationBehaviour playerAnimationBehaviour;
    public PlayerMovementBehaviour playerMovementBehaviour;

    [Header("Input Setting")]
    public PlayerInput input;
    private bool sprintPressed = false;

    [Header("Movements")]
    public float velocity = 0f;
    [SerializeField] float acceleration = 0.1f;
    [SerializeField] float decceleration = 1f;
    [SerializeField] float slideDecceleration = 1f;
    [SerializeField] float normalRunSpeed = 7f;
    [SerializeField] float sprintSpeed = 10;
    private Vector3 rawInputMovement;
    private bool isGrounded = true;
    private float slideTime = 1f;
    private float slideCountDown = 0;
    private bool isCrouching = false;
    private bool isMoving = false;

    //INPUT ACTION SYSTEM
    public void OnMovement(InputAction.CallbackContext value)
    {
        isMoving = false;
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        if(rawInputMovement.magnitude > 0) isMoving = true;
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
        if (value.started)
        {
            if (isGrounded)
            {
                playerAnimationBehaviour.ResetTriggerJumpAnimation();
                playerAnimationBehaviour.TriggerJumpAnimation();
                playerMovementBehaviour.Jump();
            }
        }
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
            playerAnimationBehaviour.setIsSliding(true);
        else
            playerAnimationBehaviour.setIsSliding(false);
        //if WASD is null
        playerAnimationBehaviour.setIsMoving(isMoving);
    }

}
