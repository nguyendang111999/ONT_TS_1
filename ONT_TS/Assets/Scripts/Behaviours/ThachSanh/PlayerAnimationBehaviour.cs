using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehaviour : MonoBehaviour
{

    [Header("Component References")]
    public Animator playerAnim;

    //Aanimation String ID
    private int id_hashVelocity;
    private int id_hashIsSliding;
    private int id_hashIsCrouching;
    private int id_hashIsJumping;
    private int id_hashIsRunning;
    private int id_hashTriggerJump;
    private int id_isGrounded;

    public void SetupBehaviour()
    {
        SetupAnimationIDs();
    }

    void SetupAnimationIDs()
    {
        id_hashVelocity = Animator.StringToHash("velocity");
        id_hashIsRunning = Animator.StringToHash("isRunning");
        id_hashIsCrouching = Animator.StringToHash("isCrouching");
        id_hashTriggerJump = Animator.StringToHash("triggerJump");
        id_isGrounded = Animator.StringToHash("isGrounded");
    }

    public void UpdateVelocity(float velocity)
    {
        playerAnim.SetFloat(id_hashVelocity, velocity);
    }

    public void PlayRunAnimation(bool isRunning)
    {
        playerAnim.SetBool(id_hashIsRunning, isRunning);
    }

    public void PlayCrouchAnimation(bool isCrouching){
        playerAnim.SetBool(id_hashIsCrouching, isCrouching);
    }
    public void TriggerJumpAnimation(){
        playerAnim.SetTrigger(id_hashTriggerJump);
    }
    public void ResetTriggerJumpAnimation(){
        playerAnim.ResetTrigger(id_hashTriggerJump);
    }
    public void SetIsGrounded(bool isGrounded){
        playerAnim.SetBool(id_isGrounded, isGrounded);
    }
}
