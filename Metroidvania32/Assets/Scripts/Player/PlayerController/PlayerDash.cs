using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerComponents components;

    private PlayerInputHandler input;
    private PlayerVelocity velocity;
    private PlayerCollision collision;

    [Header("Dash")]
    [SerializeField] private float dashForce = 12f;
    [SerializeField] private float maxDashTime = 0.1f;

    [Header("Cooldown")]
    [SerializeField] private float dashCooldownDuration = 0.5f;

    private float dashCooldownTimer;
    private float dashTimer;

    private bool dashRequested;
    private bool dashConsumed;

    public bool IsDashing { get; private set; }

    private void Start()
    {
        input = components.playerInput;
        velocity = components.playerVelocity;
        collision = components.playerCollision;
    }

    private void FixedUpdate()
    {
        UpdateInput();

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.fixedDeltaTime;
        }
    }

    private void UpdateInput()
    {
        if (input.isDashing && !dashConsumed)
        {
            dashRequested = true;
            dashConsumed = true;
        }

        if (!input.isDashing)
        {
            dashConsumed = false;
        }
    }

    public bool ShouldStartDash()
    {
        return dashRequested
            && dashCooldownTimer <= 0f
            && InventoryDataManager.Instance.currentInventoryData.unlockedUpgrades.Contains("dash");
    }

    public void StartDash()
    {
        dashRequested = false;

        IsDashing = true;
        dashTimer = 0f;

        float dashDirection = components.FacingDirection;

        velocity.SetHorizontalSpeed(dashForce * dashDirection);
        velocity.SetVerticalSpeed(0f);
        components.playerGravity.DisableGravity();
        // if (has element equipped to dash)
        // components.playerHealth.DisableDamage();
    }

    public void DashUpdate()
    {
        if (!IsDashing)
            return;

        dashTimer += Time.fixedDeltaTime;

        float dashDirection = components.FacingDirection;
        velocity.SetHorizontalSpeed(dashForce * dashDirection);
        Debug.Log($"Set Speed to {dashForce * dashDirection}");
    }

    public bool ShouldEndDash()
    {
        // if (has element equipped to dash)
        return dashTimer >= maxDashTime
            || collision.HitWall;
        // else
        // return dashTimer >= maxDashTime
            // || collision.HitWall
            // || collision.HitEnemy
    }

    public void EndDash()
    {
        IsDashing = false;

        dashCooldownTimer = dashCooldownDuration;

        velocity.SetHorizontalSpeed(0f);
        components.playerGravity.EnableGravity();
        // components.playerHealth.EnableDamage();
    }
}