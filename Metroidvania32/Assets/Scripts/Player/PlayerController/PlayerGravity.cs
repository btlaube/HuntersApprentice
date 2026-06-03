using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerVelocity velocity;
    [SerializeField] private PlayerComponents components;
    [SerializeField] private PlayerCollision collision;
    [SerializeField] private PlayerInputHandler input;
    [SerializeField] private JumpController jumpController;

    void Start()
    {
        velocity.SetGravityVelocity(new Vector2(0f, -20.0f));
    }

    void Update()
    {
        if (velocity == null || components == null || collision == null)
            return;
        // if (collision.isGrounded)
        // {
        //     velocity.SetGravityVelocity(Vector2.zero);
        // }
        // else
        // {
        //     velocity.SetGravityVelocity(new Vector2(0f, -20.0f));
        // }
    }
}
