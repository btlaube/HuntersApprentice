using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input")]
    // private Vector2 readHorizontalInput => controls.Player.Move.ReadValue<Vector2>();
    public Vector2 horizontalInput => controls.Player.Move.ReadValue<Vector2>();// { get; private set; }
    public Vector2 verticalInput { get; private set; }
    public bool isJumping { get; private set; }
    // public bool jumpPressed;// { get; private set; }
    public bool isAttacking { get; private set; }
    public bool isMeleeAttacking { get; private set; }
    public bool isRangedAttacking { get; private set; }
    public bool isDashing { get; private set; }

    // public bool inputEnabled { get; private set; }
    public bool inputEnabled => GameStateManager.Instance.GameplayEnabled;
    public event Action OnInteract;
    public event Action OnMelee;
    public event Action OnRanged;
    // public event Action OnDash;

    private PlayerControls controls;

    // public PlayerInputHandler(PlayerControls controls)
    // {
    //     this.controls = controls;

    //     controls.Player.Move.performed += ctx =>
    //     {
    //         horizontalInput = ctx.ReadValue<Vector2>();
    //     };

    //     controls.Player.Move.canceled += ctx =>
    //     {
    //         horizontalInput = Vector2.zero;
    //     };

    //     controls.Player.Jump.performed += ctx =>
    //     {
    //         isJumping = true;
    //     };

    //     controls.Player.Jump.canceled += ctx =>
    //     {
    //         isJumping = false;
    //     };
    // }

    void Awake()
    {
        // controls = new PlayerControls();
    }

    void Start()
    {
        // inputEnabled = true;
    }

    // void OnEnable()
    // {
    //     controls.Enable();

    //     controls.Player.Move.performed += OnMove;
    //     controls.Player.Move.canceled += OnMove;

    //     controls.Player.Jump.performed += OnJumpPerformed;
    //     controls.Player.Jump.canceled += OnJumpCanceled;

    //     controls.Player.Attack.performed += OnAttackPerformed;
    //     controls.Player.Attack.canceled += OnAttackCanceled;
    // }

    // void OnDisable()
    // {
    //     controls.Disable();

    //     controls.Player.Move.performed -= OnMove;
    //     controls.Player.Move.canceled -= OnMove;

    //     controls.Player.Jump.performed -= OnJumpPerformed;
    //     controls.Player.Jump.canceled -= OnJumpCanceled;

    //     controls.Player.Attack.performed -= OnAttackPerformed;
    //     controls.Player.Attack.canceled -= OnAttackCanceled;
    // }

    public void AssignControls(PlayerControls newControls)
    {
        // Unsubscribe from old controls
        if (controls != null)
        {
            controls.Player.Move.performed -= OnMove;
            controls.Player.Move.canceled -= OnMove;

            controls.Player.Jump.performed -= OnJumpPerformed;
            controls.Player.Jump.canceled -= OnJumpCanceled;

            // controls.Player.Attack.performed -= OnAttackPerformed;
            // controls.Player.Attack.canceled -= OnAttackCanceled;
            controls.Player.Melee.performed -= OnMeleePerformed;
            controls.Player.Melee.canceled -= OnMeleeCanceled;

            controls.Player.Ranged.performed -= OnRangedPerformed;
            controls.Player.Ranged.canceled -= OnRangedCanceled;

            controls.Player.Interact.performed -= OnInteractPressed;
            
            controls.Player.Dash.performed -= OnDashPerformed;
            controls.Player.Dash.canceled -= OnDashCanceled;

            controls.Disable();
        }

        // Assign new controls
        controls = newControls;

        if (controls != null)
        {
            controls.Enable();

            controls.Player.Move.performed += OnMove;
            controls.Player.Move.canceled += OnMove;

            controls.Player.Jump.performed += OnJumpPerformed;
            controls.Player.Jump.canceled += OnJumpCanceled;

            // controls.Player.Attack.performed += OnAttackPerformed;
            // controls.Player.Attack.canceled += OnAttackCanceled;
            controls.Player.Melee.performed += OnMeleePerformed;
            controls.Player.Melee.canceled += OnMeleeCanceled;

            controls.Player.Ranged.performed += OnRangedPerformed;
            controls.Player.Ranged.canceled += OnRangedCanceled;

            controls.Player.Interact.performed += OnInteractPressed;

            controls.Player.Dash.performed += OnDashPerformed;
            controls.Player.Dash.canceled += OnDashCanceled;
            // controls.Player.Interact.started += ctx => Debug.Log("STARTED");
            // controls.Player.Interact.performed += ctx => Debug.Log("PERFORMED");
            // controls.Player.Interact.canceled += ctx => Debug.Log("CANCELED");
        }
    }

    public void UnassignControls()
    {
        AssignControls(null);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // if (!inputEnabled) return;
        // horizontalInput = context.ReadValue<Vector2>();
        // horizontalInput = readHorizontalInput;
    }

    public Vector2 GetMoveInput()
    {
        return horizontalInput;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        // if (!inputEnabled) return;
        isJumping = true;
        // jumpPressed = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        // if (!inputEnabled) return;
        isJumping = false;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        // Attack input is handled in PlayerAttack via PlayerInputHandler reference
        isAttacking = true;
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        // Attack input is handled in PlayerAttack via PlayerInputHandler reference
        isAttacking = false;
    }

    private void OnMeleePerformed(InputAction.CallbackContext contect)
    {
        // OnMelee?.Invoke();
        isMeleeAttacking = true;
    }

    private void OnMeleeCanceled(InputAction.CallbackContext contect)
    {
        // OnMelee?.Invoke();
        isMeleeAttacking = false;
    }

    private void OnRangedPerformed(InputAction.CallbackContext contect)
    {
        // OnRanged?.Invoke();
        isRangedAttacking = true;
    }

    private void OnRangedCanceled(InputAction.CallbackContext contect)
    {
        // OnRanged?.Invoke();
        isRangedAttacking = false;
    }

    private void OnDashPerformed(InputAction.CallbackContext contect)
    {
        // OnDash?.Invoke();
        isDashing = true;
    }

    private void OnDashCanceled(InputAction.CallbackContext contect)
    {
        isDashing = false;
    }

    private void OnInteractPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Interect press recieved");
        OnInteract?.Invoke();
    }

    public void EnableInput()
    {
        // inputEnabled = true;
    }

    public void DisableInput()
    {
        // inputEnabled = false;
        // horizontalInput = Vector2.zero;
        verticalInput = Vector2.zero;
        isJumping = false;
    }

    // private void OnJump(InputAction.CallbackContext context)
    // {
    //     // Jump input is handled in JumpController via PlayerInputHandler reference
    //     // verticalInput.y = context.ReadValue<float>();
    //     // verticalInput = context.ReadValue<Vector2>();
    //     isJumping = context.ReadValueAsButton();
    // }
    
}