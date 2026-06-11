using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerComponents components;

    private PlayerInputHandler input;
    private PlayerVelocity velocity;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;

    [Header("Acceleration")]
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 80f;

    private bool facingRight = true;
    private bool facingLocked;

    private float moveInput;
    private float currentHorizontalSpeed;

    public float MoveInput => moveInput;
    public float CurrentSpeed => currentHorizontalSpeed;

    private void Start()
    {
        input = components.playerInput;
        velocity = components.playerVelocity;

        input.EnableInput();
    }

    private void FixedUpdate()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (input == null)
            return;

        moveInput = input.horizontalInput.x;

        if (!facingLocked && Mathf.Abs(moveInput) > 0.01f)
        {
            components.FacingDirection =
                moveInput > 0 ? 1 : -1;

            HandleFacing();
        }
    }

    public void MovementUpdate()
    {
        if (!input.inputEnabled)
        {
            currentHorizontalSpeed = 0f;
            components.playerVelocity.SetHorizontalSpeed(0f);
            return;
        }

        float targetSpeed = moveInput * moveSpeed;

        float movementAcceleration =
            Mathf.Abs(targetSpeed) > 0.01f
                ? acceleration
                : deceleration;

        currentHorizontalSpeed = Mathf.MoveTowards(
            currentHorizontalSpeed,
            targetSpeed,
            movementAcceleration * Time.fixedDeltaTime
        );

        components.playerVelocity.SetHorizontalSpeed(currentHorizontalSpeed);
    }

    public void StopMovement()
    {
        currentHorizontalSpeed = 0f;
        components.playerVelocity.SetHorizontalSpeed(0f);
    }

    public bool ShouldRun()
    {
        return Mathf.Abs(moveInput) > 0.01f;
    }

    public bool ShouldIdle()
    {
        return Mathf.Abs(moveInput) <= 0.01f;
    }

    public bool IsMoving()
    {
        return Mathf.Abs(currentHorizontalSpeed) > 0.1f;
    }

    public float GetHorizontalSpeed()
    {
        return currentHorizontalSpeed;
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
}