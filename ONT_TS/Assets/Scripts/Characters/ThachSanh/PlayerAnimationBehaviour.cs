using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehaviour : MonoBehaviour
{
    [Header("Component References")]
    public Animator playerAnim;

    //Aanimation String ID
    private int id_HashVelocity;
    private int id_HashIsSliding;
    private int id_HashIsCrouching;
    private int id_HashIsJumping;
    private int id_HashIsMoving;
    private int id_HashTriggerJump;
    private int id_Grounded;
    private int id_MeleeAttack;
    private int id_Dodge;

    public void SetupBehaviour()
    {
        SetupAnimationIDs();
    }

    void SetupAnimationIDs()
    {
        id_HashVelocity = Animator.StringToHash("Velocity");
        id_HashIsMoving = Animator.StringToHash("isMoving");
        id_HashIsCrouching = Animator.StringToHash("isCrouching");
        id_HashTriggerJump = Animator.StringToHash("triggerJump");
        id_HashIsSliding = Animator.StringToHash("isSliding");
        id_Grounded = Animator.StringToHash("isGrounded");
        id_MeleeAttack = Animator.StringToHash("Attack");
        id_Dodge = Animator.StringToHash("Dodge");
    }

    public void UpdateVelocity(float velocity) => playerAnim.SetFloat(id_HashVelocity, velocity);
    public void PlayRunAnimation(bool isRunning) => playerAnim.SetBool(id_HashIsMoving, isRunning);
    public void PlayCrouchAnimation(bool isCrouching) => playerAnim.SetBool(id_HashIsCrouching, isCrouching);
    public void TriggerJumpAnimation() => playerAnim.SetTrigger(id_HashTriggerJump);
    public void ResetTriggerJumpAnimation() => playerAnim.ResetTrigger(id_HashTriggerJump);
    public void SetIsSliding(bool isSliding) => playerAnim.SetBool(id_HashIsSliding, isSliding);
    public void SetIsMoving(bool isMoving) => playerAnim.SetBool(id_HashIsMoving, isMoving);
    public void SetIsGrounded(bool isGrounded) => playerAnim.SetBool(id_Grounded, isGrounded);
    public void SetMeleeAttack(bool val) => playerAnim.SetBool(id_MeleeAttack, val);
    public void SetDodgeParameter(bool isDodge) => playerAnim.SetBool(id_Dodge, isDodge);
}
