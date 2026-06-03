using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInputHandler input;
    [SerializeField] private PlayerVelocity velocity;
    [SerializeField] private PlayerComponents components;

    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed = 8f;

    [Header("Acceleration")]
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 80f;
    [SerializeField] private bool facingRight = true;

    private bool facingLocked;
    private float moveInput;

    /*
        Internal movement state.
            Horizontal speed stored separately from Rigidbody 
            velocity so movement stays clean and controllable.
    */
    private float currentHorizontalSpeed;

    void Start()
    {
        input = MasterInputHandler.Instance.playerInput;
        input.EnableInput();
        // Debug.Log(input.horizontalInput);
        // velocity = components.velocity;
        // HandleHorizontalMovement();
    }

    private void FixedUpdate()
    {
        if (!input.inputEnabled) 
        {
            velocity.SetHorizontalSpeed(0f);
            return;
        }
        if (input == null || velocity == null)
            return;

        HandleHorizontalMovement();
    }

    private void HandleHorizontalMovement()
    {
        /*
            Read player input.
                Usually:
                -1 = left
                0 = idle
                1 = right
        */
        moveInput = input.horizontalInput.x;

        /*
            Calculate desired movement speed.
        */
        float targetSpeed = moveInput * moveSpeed;

        /*
            Choose acceleration or deceleration.
                If player is pressing movement:
                    accelerate
                Otherwise:
                    decelerate
        */
        float movementAcceleration =
            Mathf.Abs(targetSpeed) > 0.01f
            ? acceleration
            : deceleration;

        /*
            Smoothly move toward target speed.
        */
        currentHorizontalSpeed = Mathf.MoveTowards(
            currentHorizontalSpeed,
            targetSpeed,
            movementAcceleration * Time.fixedDeltaTime
        );

        /*
            Send final horizontal movement
            into PlayerVelocity.
        */
        velocity.SetHorizontalSpeed(currentHorizontalSpeed);

        // Handle facing direction based on movement input.
        if (!facingLocked)
        {
            HandleFacing();
        }

        /*
            Optional debug.
        */
        // Debug.Log(
        //     $"Input: {moveInput} | " +
        //     $"Target: {targetSpeed} | " +
        //     $"Current: {currentHorizontalSpeed}"
        // );
    }
    public void EnableFacing()
    {
        facingLocked = false;
    }

    public void DisableFacing()
    {
        facingLocked = true;
    }


    private void HandleFacing()
    {
        if (moveInput > 0f && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0f && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    /*
        Optional helper methods.
    */
    public bool IsMoving()
    {
        return Mathf.Abs(currentHorizontalSpeed) > 0.1f;
    }

    public float GetHorizontalSpeed()
    {
        return currentHorizontalSpeed;
    }

    public void StopMovement()
    {
        currentHorizontalSpeed = 0f;
        velocity.SetHorizontalSpeed(0f);
    }
}