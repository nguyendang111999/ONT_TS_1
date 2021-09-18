using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input")]
public class InputReader : ScriptableObject, PlayerInput.IGamePlayActions, PlayerInput.IMenuActions, PlayerInput.IDialogueActions
{
    #region Action
    //Gameplay
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction<Vector2, bool> RotateCameraEvent = delegate { };
    public event UnityAction OnAimEvent = delegate { };
    public event UnityAction StartRunningEvent = delegate { };
    public event UnityAction StopRunningEvent = delegate { };

    //Dodge event tap and double tap
    public event UnityAction TapDodgeEventPerformed = delegate { };
    public event UnityAction TapDodgeEventCancel = delegate { };
    public event UnityAction DoubleTapDodgeEventStarted = delegate { };
    public event UnityAction DoubleTapDodgeEventPerformed = delegate { };
    public event UnityAction DoubleTapDodgeEventCancel = delegate { };

    //Jump events
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCanceledEvent = delegate { };

    //Crouch events
    public event UnityAction CrouchEvent = delegate { };
    public event UnityAction CrouchStopEvent = delegate { };

    //Interact event
    public event UnityAction InteractEvent = delegate { };

    //Equip Weapon event
    public event UnityAction EquipWeaponEvent = delegate { };


    //Escape event
    public event UnityAction EscapeEvent = delegate { };
    //Open Ingame Menu
    public event UnityAction OpenIngameMenu = delegate { };

    //Skip event
    public event UnityAction SkipEvent = delegate { };

    //Open inventory event
    public event UnityAction OpenInventoryEvent = delegate { };
    public event UnityAction CloseInventoryEvent = delegate { };

    //Melee attack event
    public event UnityAction AttackEvent = delegate { };
    public event UnityAction AttackCanceledEvent = delegate { };
    //Skills event
    public event UnityAction Skill1Event = delegate { };
    public event UnityAction Skill2Event = delegate { };
    //Heavy Attack
    public event UnityAction TapHeavyAttackEvent = delegate { };
    public event UnityAction TapHeavyAttackCanceled = delegate { };
    public event UnityAction HoldHeavyAttackStarted = delegate { };
    public event UnityAction HoldHeavyAttackPerformed = delegate { };
    public event UnityAction HoldHeavyAttackCanceled = delegate { };

    //Ability
    public event UnityAction EarthAbilityEvent = delegate { };
    public event UnityAction EarthAbilityCancelEvent = delegate { };
    public event UnityAction LifeAbilityEvent = delegate { };
    public event UnityAction LifeAbilityCancelEvent = delegate { };
    #endregion

    private PlayerInput playerInput;

    #region Setup Functions
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
        Cursor.visible = true;
        playerInput.Menu.Enable();
        playerInput.GamePlay.Disable();
        playerInput.Dialogue.Enable();
    }
    public void EnableMenuInput()
    {
        Cursor.visible = true;
        playerInput.Menu.Enable();
        playerInput.GamePlay.Disable();
        playerInput.Dialogue.Disable();
    }
    public void EnableGameplayInput()
    {
        Cursor.visible = false;
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
    #endregion

    #region Raise Unity Action for each input

    //Implements from PlayerInterface
    public void OnCamInput(InputAction.CallbackContext context)
    {
        RotateCameraEvent.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    private bool IsDeviceMouse(InputAction.CallbackContext context)
    {
        // bool a = context.control.device.name == "Mouse";
        // Debug.Log("Is Mouse: " + a);
        return context.control.device.name == "Mouse";
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            AttackEvent.Invoke();
        }
    }
    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (context.interaction is HoldInteraction)
                {
                    HoldHeavyAttackStarted.Invoke();
                }
                break;
            case InputActionPhase.Performed:
                if (context.interaction is HoldInteraction)
                {
                    HoldHeavyAttackPerformed?.Invoke();
                }
                else
                {
                    TapHeavyAttackEvent?.Invoke();
                }
                break;
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                TapDodgeEventPerformed.Invoke();
                break;
            case InputActionPhase.Performed:
                DoubleTapDodgeEventPerformed.Invoke();
                break;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            JumpEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled)
            JumpCanceledEvent.Invoke();
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            InteractEvent.Invoke();
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            OpenInventoryEvent.Invoke();
    }
    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            CloseInventoryEvent.Invoke();
    }

    public void OnEarthAbility(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            EarthAbilityEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled)
            EarthAbilityCancelEvent.Invoke();
    }

    public void OnLifeAbility(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            LifeAbilityEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled)
            LifeAbilityCancelEvent.Invoke();
    }

    public void OnChoose(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {

        }
    }

    public void OnSkip(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SkipEvent.Invoke();
        }
    }

    public void OnEquipWeapon(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EquipWeaponEvent.Invoke();
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            EscapeEvent.Invoke();
        }
    }

    public void OnIngameMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OpenIngameMenu.Invoke();
        }
    }
    #endregion
}
