using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerComponents components;

    private PlayerInputHandler input;
    private PlayerVelocity velocity;
    private PlayerCollision collision;

    [Header("Initial Jump")]
    [SerializeField] private float jumpForce = 12f;

    [Header("Sustained Jump")]
    [SerializeField] private float sustainedJumpForce = 30f;

    [SerializeField] private float maxJumpTime = 0.2f;

    [SerializeField] private AnimationCurve jumpForceCurve =
        AnimationCurve.EaseInOut(3, 2, 1, 0);

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimer;

    [Header("Jump Buffer")]
    [SerializeField] private float saveJumpThreshold = 0.1f;

    public bool shouldJump;
    public float saveJumpTimer;

    [Header("Jump State")]
    public bool JumpStarted { get; private set; }

    private float jumpTimer;
    private bool jumpConsumed;

    private void Start()
    {
        input = components.playerInput;
        velocity = components.playerVelocity;
        collision = components.playerCollision;
    }

    private void FixedUpdate()
    {
        UpdateJumpInput();
    }

    public void UpdateJumpInput()
    {
        // Coyote Time
        if (collision.isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.fixedDeltaTime;
        }

        // Jump Buffer
        if (input.isJumping && !shouldJump && !JumpStarted)
        {
            shouldJump = true;
            saveJumpTimer = 0f;
        }

        if (shouldJump)
        {
            saveJumpTimer += Time.fixedDeltaTime;

            if (saveJumpTimer > saveJumpThreshold)
            {
                shouldJump = false;
            }
        }

        // Reset consumption when jump released
        if (!input.isJumping)
        {
            jumpConsumed = false;
        }
    }

    public bool ShouldStartJump()
    {
        return shouldJump
            && coyoteTimer > 0f
            && !jumpConsumed;
    }

    public void StartJump()
    {
        velocity.SetVerticalSpeed(jumpForce);

        JumpStarted = true;
        jumpTimer = 0f;

        shouldJump = false;
        jumpConsumed = true;
    }

    public void JumpUpdate()
    {
        if (!JumpStarted)
            return;

        jumpTimer += Time.fixedDeltaTime;

        if (input.isJumping && jumpTimer < maxJumpTime)
        {
            float sustainedForce =
                sustainedJumpForce *
                jumpForceCurve.Evaluate(
                    jumpTimer / maxJumpTime);

            velocity.SetVerticalSpeed(
                Mathf.Max(jumpForce, sustainedForce));
        }
    }

    public bool ShouldFall()
    {
        return collision.hitCeiling
            || !input.isJumping
            || jumpTimer >= maxJumpTime;
    }

    public void EndJump()
    {
        velocity.SetVerticalSpeed(0f);
        
        JumpStarted = false;
        jumpTimer = 0f;
    }
}