using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerComponents components;

    [Header("Collision")]
    public LayerMask collisionLayer;

    [Header("Ray Settings")]
    public float skinWidth = 0.02f;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    public float rayLength = 0.1f;

    [Header("Collision States")]
    public bool isGrounded;
    public bool hitCeiling;
    public bool hitLeftWall;
    public bool hitRightWall;
    public bool HitWall => hitLeftWall || hitLeftWall;

    private Bounds bounds;

    void Update()
    {
        UpdateBounds();

        CheckVerticalCollisions();
        CheckHorizontalCollisions();
    }

    void UpdateBounds()
    {
        bounds = components.collider.bounds;
        bounds.Expand(skinWidth * -2f);
    }

    void CheckVerticalCollisions()
    {
        isGrounded = false;
        hitCeiling = false;

        float raySpacing =
            bounds.size.x / (verticalRayCount - 1);

        Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 topLeft = new Vector2(bounds.min.x, bounds.max.y);

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 bottomRayOrigin =
                bottomLeft + Vector2.right * (raySpacing * i);

            Vector2 topRayOrigin =
                topLeft + Vector2.right * (raySpacing * i);

            RaycastHit2D groundHit = Physics2D.Raycast(
                bottomRayOrigin,
                Vector2.down,
                rayLength,
                collisionLayer
            );

            RaycastHit2D ceilingHit = Physics2D.Raycast(
                topRayOrigin,
                Vector2.up,
                rayLength,
                collisionLayer
            );

            if (groundHit)
            {
                isGrounded = true;
            }

            if (ceilingHit)
            {
                hitCeiling = true;
            }

            Debug.DrawRay(
                bottomRayOrigin,
                Vector2.down * rayLength,
                groundHit ? Color.green : Color.red
            );

            Debug.DrawRay(
                topRayOrigin,
                Vector2.up * rayLength,
                ceilingHit ? Color.green : Color.red
            );
        }
    }

    void CheckHorizontalCollisions()
    {
        hitLeftWall = false;
        hitRightWall = false;

        float raySpacing =
            bounds.size.y / (horizontalRayCount - 1);

        Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 leftRayOrigin =
                bottomLeft + Vector2.up * (raySpacing * i);

            Vector2 rightRayOrigin =
                bottomRight + Vector2.up * (raySpacing * i);

            RaycastHit2D leftHit = Physics2D.Raycast(
                leftRayOrigin,
                Vector2.left,
                rayLength,
                collisionLayer
            );

            RaycastHit2D rightHit = Physics2D.Raycast(
                rightRayOrigin,
                Vector2.right,
                rayLength,
                collisionLayer
            );

            if (leftHit)
            {
                hitLeftWall = true;
            }

            if (rightHit)
            {
                hitRightWall = true;
            }

            Debug.DrawRay(
                leftRayOrigin,
                Vector2.left * rayLength,
                leftHit ? Color.green : Color.red
            );

            Debug.DrawRay(
                rightRayOrigin,
                Vector2.right * rayLength,
                rightHit ? Color.green : Color.red
            );
        }
    }
}