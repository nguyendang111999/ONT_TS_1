using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input")]
public class InputReader : ScriptableObject, PlayerInput.IGamePlayActions, PlayerInput.IMenuActions, PlayerInput.IDialogueActions
{
    //Gameplay
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction<Vector2> RotateCameraEvent = delegate { };
    public event UnityAction OnAimEvent = delegate { };
    public event UnityAction StartRunningEvent = delegate { };
    public event UnityAction StopRunningEvent = delegate { };
    public event UnityAction DodgeEvent = delegate { };
    public event UnityAction JumpCanceledEvent = delegate { };
    public event UnityAction CrouchEvent = delegate { };
    public event UnityAction CrouchStopEvent = delegate { };
    public event UnityAction AttackEvent = delegate { };
    public event UnityAction AttackCanceledEvent = delegate { };
    public event UnityAction Skill1Event = delegate { };
    public event UnityAction Skill2Event = delegate { };
    public event UnityAction HeavyAttackEvent = delegate { };
    public event UnityAction HeavyAttackCanceledEvent = delegate { };

    private PlayerInput playerInput;

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();
            playerInput.Menu.SetCallbacks(this);
            playerInput.GamePlay.SetCallbacks(this);
            playerInput.Dialogue.SetCallbacks(this);
        }
        playerInput.GamePlay.Enable();
    }
    private void OnDisable()
    {
        DisableAllInput();
    }

    public void EnableDialogueInput()
    {
        playerInput.Menu.Enable();
        playerInput.GamePlay.Disable();
        playerInput.Dialogue.Enable();
    }
    public void EnableMenuInput()
    {
        playerInput.Menu.Enable();
        playerInput.GamePlay.Disable();
        playerInput.Dialogue.Disable();
    }
    public void EnableGameplayInput()
    {
        playerInput.Menu.Disable();
        playerInput.GamePlay.Enable();
        playerInput.Dialogue.Disable();
    }
    public void DisableAllInput()
    {
        playerInput.Menu.Disable();
        playerInput.GamePlay.Disable();
        playerInput.Dialogue.Disable();
    }


    //Implements from PlayerInterface
    void PlayerInput.IGamePlayActions.OnCamInput(InputAction.CallbackContext context)
    {
        RotateCameraEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                AttackEvent.Invoke();
                break;
            case InputActionPhase.Canceled:
                AttackCanceledEvent.Invoke();
                break;
        }
    }
    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                HeavyAttackEvent.Invoke();
                break;
            case InputActionPhase.Canceled:
                HeavyAttackCanceledEvent.Invoke();
                break;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            CrouchEvent.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            CrouchStopEvent.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            DodgeEvent.Invoke();
    }

    public void OnMovements(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                StartRunningEvent.Invoke();
                break;
            case InputActionPhase.Canceled:
                StopRunningEvent.Invoke();
                break;
        }
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        Skill1Event.Invoke();
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        Skill2Event.Invoke();
    }

    public void OnChoose(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

}
