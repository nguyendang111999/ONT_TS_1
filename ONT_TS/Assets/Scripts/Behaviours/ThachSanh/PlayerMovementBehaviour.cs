using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{

    [Header("Component References")]
    public CharacterController controller;
    public Transform cam;

    [Header("Movement Settings")]
    public float runSpeed = 3f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public float gravity = 9.81f;
    public float jumpHeight = 3f;
    public Vector3 direction;
    private Vector3 fallVelocity;

    public bool isGrounded = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        checkIfGrounded();
        MoveCharacter();
        controller.Move(fallVelocity * Time.deltaTime);
    }

    public void UpdateMovementData(Vector3 rawInputMovement)
    {
        direction = rawInputMovement;
    }
    public void MoveCharacter()
    {
        if (direction.magnitude > 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * runSpeed * Time.deltaTime);
        }        
    }

    public void checkIfGrounded(){
        isGrounded = controller.isGrounded;
        fallVelocity.y -= gravity * Time.deltaTime;
        if(isGrounded && fallVelocity.y < 0){
            fallVelocity.y = -2f;
        }
        controller.Move(fallVelocity * Time.deltaTime);
        Debug.Log("isGrounded: " + isGrounded);
    }

    public void Jump(){
        fallVelocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        controller.Move(fallVelocity * Time.deltaTime);
    }
}
