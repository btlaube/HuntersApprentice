using UnityEngine;

public class PlayerVelocity : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerComponents components;

    /*
        These are the "layers" of movement.

        Different systems write to different velocity channels.

        Example:
        - PlayerMovement 
        - PlayerGravity controls falling
        - Knockback adds external forces
    */

    // Standard movement velocity
    [SerializeField] private Vector2 movementVelocity;

    // Gravity contribution
    [SerializeField] private Vector2 gravityVelocity;

    // Temporary external forces
    [SerializeField] private Vector2 externalVelocity;

    // Hard override velocity (dash, cutscene, launch pads, etc.)
    [SerializeField] private Vector2 overrideVelocity;

    // Whether override velocity is active
    [SerializeField] private bool usingOverrideVelocity;

    // Final combined velocity
    // public Vector2 FinalVelocity { get; private set; }
    [SerializeField] private Vector2 FinalVelocity;

    /*
        PUBLIC API
        Other systems call these methods.
    */

    // void Start()
    // {
    //     SetGravityVelocity(new Vector2(0f, -20.0f));
    // }

    // Horizontal movement system writes here
    public void SetMovementVelocity(Vector2 velocity)
    {
        movementVelocity = velocity;
    }

    // Gravity system writes here
    public void SetGravityVelocity(Vector2 velocity)
    {
        gravityVelocity = velocity;
    }

    // Knockback / recoil / moving platforms
    public void AddExternalVelocity(Vector2 velocity)
    {
        externalVelocity += velocity;
    }

    // Dash / launch / scripted movement
    public void SetOverrideVelocity(Vector2 velocity)
    {
        overrideVelocity = velocity;
        usingOverrideVelocity = true;
    }

    public void ClearOverrideVelocity()
    {
        usingOverrideVelocity = false;
    }

    /*
        OPTIONAL HELPERS
    */

    public void SetVerticalSpeed(float ySpeed)
    {
        movementVelocity.y = ySpeed;
    }

    public void SetHorizontalSpeed(float xSpeed)
    {
        movementVelocity.x = xSpeed;
    }

    public void StopHorizontal()
    {
        movementVelocity.x = 0f;
    }

    public void StopVertical()
    {
        movementVelocity.y = 0f;
        gravityVelocity.y = 0f;
    }

    /*
        MAIN UPDATE
    */

    private void FixedUpdate()
    {
        CalculateFinalVelocity();
        ApplyVelocity();
        DecayExternalVelocity();
    }

    /*
        COMBINE ALL VELOCITY SOURCES
    */

    private void CalculateFinalVelocity()
    {
        if (usingOverrideVelocity)
        {
            FinalVelocity = overrideVelocity;
            return;
        }

        FinalVelocity =
            movementVelocity +
            gravityVelocity +
            externalVelocity;

        // Debug.Log($"Movement: {movementVelocity} | Gravity: {gravityVelocity} | External: {externalVelocity} | Final: {FinalVelocity}");
    }

    /*
        ACTUALLY MOVE THE RIGIDBODY
    */

    private void ApplyVelocity()
    {
        components.rb.linearVelocity = FinalVelocity;
        // Debug.Log($"Applied Velocity: {components.rb.linearVelocity}");
    }

    /*
        EXTERNAL FORCES FADE OUT OVER TIME
        Prevents permanent knockback accumulation.
    */

    private void DecayExternalVelocity()
    {
        externalVelocity = Vector2.Lerp(
            externalVelocity,
            Vector2.zero,
            10f * Time.fixedDeltaTime
        );
    }

    /*
        DEBUGGING
    */

    public Vector2 GetMovementVelocity()
    {
        return movementVelocity;
    }

    public Vector2 GetGravityVelocity()
    {
        return gravityVelocity;
    }

    public Vector2 GetExternalVelocity()
    {
        return externalVelocity;
    }

    public Vector2 GetFinalVelocity()
    {
        return FinalVelocity;
    }

    public bool IsUsingOverrideVelocity()
    {
        return usingOverrideVelocity;
    }
}