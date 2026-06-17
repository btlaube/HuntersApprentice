using UnityEngine;

public class PlayerWallCling : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerComponents components;

    private PlayerInputHandler input;
    private PlayerVelocity velocity;
    private PlayerCollision collision;

    [Header("WallCling")]
    [SerializeField] private float wallSlideSpeed = 8f;


    private void Start()
    {
        input = components.playerInput;
        velocity = components.playerVelocity;
        collision = components.playerCollision;
    }

    public bool CanWallCling()
    {
        return collision.HitWall
            && !collision.isGrounded;
            // && velocity.GetMovementVelocity().y <= 0f;
    }

    public void WallClingUpdate()
    {
        // velocity.SetVerticalSpeed(
        //     Mathf.Max(
        //         velocity.GetMovementVelocity().y,
        //         -wallSlideSpeed));
        components.playerGravity.SetGravity(-wallSlideSpeed);
    }

    public void EndWallCling()
    {
        components.playerGravity.EnableGravity();
    }        
}
