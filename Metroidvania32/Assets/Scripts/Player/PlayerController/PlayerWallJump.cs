using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerComponents components;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpHorizontalForce = 8f;
    [SerializeField] private float wallJumpVerticalForce = 12f;

    [SerializeField] private float wallJumpDuration = 0.25f;

    public bool WallJumpStarted { get; private set; }

    private float wallJumpTimer;

    public bool ShouldStartWallJump()
    {
        return components.playerInput.isJumping
            && components.playerCollision.HitWall
            && !components.playerCollision.isGrounded;
    }

    public void StartWallJump()
    {
        int wallDirection = components.playerCollision.WallDirection;

        components.playerVelocity.SetHorizontalSpeed(
            -wallDirection * wallJumpHorizontalForce);

        components.playerVelocity.SetVerticalSpeed(
            wallJumpVerticalForce);

        WallJumpStarted = true;
        wallJumpTimer = 0f;
    }

    public void WallJumpUpdate()
    {
        if (!WallJumpStarted)
            return;

        int wallDirection = components.playerCollision.WallDirection;

        // components.playerVelocity.SetHorizontalSpeed(
        //     -wallDirection * wallJumpHorizontalForce);

        // components.playerVelocity.SetVerticalSpeed(
        //     wallJumpVerticalForce);

        wallJumpTimer += Time.fixedDeltaTime;
    }

    public bool ShouldEndWallJump()
    {
        return wallJumpTimer >= wallJumpDuration;
    }

    public void EndWallJump()
    {
        components.playerVelocity.SetHorizontalSpeed(0.0f);
        components.playerVelocity.SetVerticalSpeed(0.0f);
        WallJumpStarted = false;
        wallJumpTimer = 0f;
    }
}