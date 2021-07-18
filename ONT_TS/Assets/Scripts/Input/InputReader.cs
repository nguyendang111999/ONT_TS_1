using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input")]
public class InputReader : ScriptableObject, PlayerInput.IGamePlayActions, PlayerInput.IMenuActions, PlayerInput.IDialogueActions
{
    //Gameplay
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction<Vector2> rotateCamera = delegate { };
    public event UnityAction startRunning = delegate { };
    public event UnityAction stopRunning = delegate { };
    public event UnityAction jumpEvent = delegate { };
    public event UnityAction jumpCanceledEvent = delegate { };
    public event UnityAction crouchEvent = delegate { };
    public event UnityAction crouchStopEvent = delegate { };
    public event UnityAction attackEvent = delegate { };
    public event UnityAction attackCanceledEvent = delegate { };
    public event UnityAction skill1 = delegate { };
    public event UnityAction skill2 = delegate { };

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
        rotateCamera.Invoke(context.ReadValue<Vector2>());
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
                attackEvent.Invoke();
                break;
            case InputActionPhase.Canceled:
                attackCanceledEvent.Invoke();
                break;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            crouchEvent.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            crouchStopEvent.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            jumpEvent.Invoke();
    }

    public void OnMovements(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                startRunning.Invoke();
                break;
            case InputActionPhase.Canceled:
                stopRunning.Invoke();
                break;
        }
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        skill1.Invoke();
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        skill2.Invoke();
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
