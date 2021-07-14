using System;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{

    [Header("Component References")]
    public CharacterController controller;
    public Transform cam;

    [Header("Movement Settings")]
    [NonSerialized]public Vector3 movementVector;
    public float runSpeed = 3f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public float gravity = 9.81f;
    public float jumpHeight = 3f;
    private float targetAngle = 0f;
    public Vector3 direction;
    private Vector3 fallVelocity;

    private bool isGrounded = true;
    private bool isSliding = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        fallingToGround();
        MoveCharacter();
        controller.Move(fallVelocity * Time.deltaTime);
    }

    public void UpdateMovementData(Vector3 rawInputMovement)
    {
        direction = rawInputMovement;
    }
    public void MoveCharacter()
    {
        float magnitudeDir = direction.magnitude;
        if (magnitudeDir > 0.1 || runSpeed > 0)
        {
            if(magnitudeDir > 0){
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            }

            turnSmoothTime = 0.1f;
            if(isSliding) turnSmoothTime = 0.5f;
            
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            
            movementVector = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            if(isSliding) movementVector = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            controller.Move(movementVector.normalized * runSpeed * Time.deltaTime);
        }        
    }
    public void fallingToGround(){
        isGrounded = checkIfGrounded();
        fallVelocity.y -= gravity * Time.deltaTime;
        if(isGrounded && fallVelocity.y < 0){
            fallVelocity.y = -2f;
        }
        // controller.Move(fallVelocity * Time.deltaTime);
    }
    public bool checkIfGrounded(){
        return controller.isGrounded;
    }
    public void Jump(){
        fallVelocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        controller.Move(fallVelocity * Time.deltaTime);
    }
    public bool isSlide(bool slide){
        return isSliding = slide;
    }
}
