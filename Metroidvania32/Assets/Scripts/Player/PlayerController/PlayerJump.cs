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

    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimer;

    [Header("Jump Cut")]
    [SerializeField] private float jumpCutMultiplier = 0.35f;

    public bool JumpStarted { get; private set; }

    [SerializeField] private float jumpTimer;

    [SerializeField] private bool jumpConsumed;
    public bool shouldJump;
    public float saveJumpThreshold;
    private float saveJumpTimer;

    public int currentJumps;
    public int maxJumps;

    public bool JumpExpired =>
        jumpTimer >= maxJumpTime;

    public bool JumpReleased =>
        !input.isJumping;

    void Start()
    {
        // input = MasterInputHandler.Instance.playerInput;
        input = components.playerInput;
        velocity = components.playerVelocity;
        collision = components.playerCollision;

        JumpStarted = false;
        jumpTimer = 0f;
        coyoteTimer = coyoteTime;
    }

    private void FixedUpdate()
    {
        if (!input.inputEnabled)
        {
            velocity.SetVerticalSpeed(0.0f);
            return;
        }
        if (input == null || velocity == null)
            return;

        coyoteTimer = collision.isGrounded ? coyoteTime : coyoteTimer - Time.fixedDeltaTime;

        HandleJump();
        HandleVariableJump();
    }

    /*
        INITIAL JUMP
    */
    private void HandleJump()
    {
        if (input.isJumping && !shouldJump && !jumpConsumed)
        {
            shouldJump = true;
            saveJumpTimer = 0f;
        }

        if (input.isJumping && !JumpStarted && coyoteTimer > 0f && !jumpConsumed)
        {
            velocity.SetVerticalSpeed(jumpForce);
            JumpStarted = true;
            jumpTimer = 0f;
            jumpConsumed = true;
            components.animator.SetBool("IsJumping", true);
        }
        
        if (shouldJump)
        {
            saveJumpTimer += Time.fixedDeltaTime;
        }
        if (saveJumpTimer > saveJumpThreshold)
        {
            shouldJump = false;
            jumpConsumed = true;
        }

        if (!input.isJumping)
        {
            jumpConsumed = false;
        }

    }

    /*
        VARIABLE HEIGHT / SMOOTH ARC
    */
    private void HandleVariableJump()
    {
        if (collision.hitCeiling)
        {
            CutJump();
            return;
        }
        if (JumpStarted)
        {
            jumpTimer += Time.fixedDeltaTime;

            if (input.isJumping && jumpTimer < maxJumpTime)
            {
                shouldJump = false;

                float sustainedForce = sustainedJumpForce * jumpForceCurve.Evaluate(jumpTimer / maxJumpTime);
                velocity.SetVerticalSpeed(Mathf.Max(jumpForce, sustainedForce));
            }
            else
            {
                CutJump();
            }
        }
    }

    /*
        SHORT HOP / MINIMUM JUMP HEIGHT
    */
    private void CutJump()
    {
        velocity.SetVerticalSpeed(0f);

        JumpStarted = false;
        jumpTimer = 0f;
        // shouldJump = false;
        // components.animator.SetBool("IsJumping", false);
        // components.animator.SetBool("IsFalling", true);
    }

    public bool ShouldJump()
    {
        return shouldJump;
    }

    public bool JumpEnded()
    {
        return jumpTimer >= maxJumpTime || !input.isJumping;
    }

}