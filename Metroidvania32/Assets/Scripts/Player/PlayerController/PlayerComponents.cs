using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    public Animator animator { get; private set; }
    public PlayerMovement playerMovement {get; private set;}
    public PlayerVelocity playerVelocity {get; private set;}
    public AudioHandler audioHandler {get; private set;}
    public PlayerStateManager stateManager {get; private set;}
    public PlayerCollision playerCollision {get; private set;}
    public PlayerJump playerJump {get; private set;}
    public PlayerInputHandler playerInput {get; private set;}
    public PlayerAnimator playerAnimator {get; private set;}

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        stateManager = GetComponent<PlayerStateManager>();
        playerCollision = GetComponent<PlayerCollision>();
        playerJump = GetComponent<PlayerJump>();
        playerVelocity = GetComponent<PlayerVelocity>();
        playerInput = MasterInputHandler.Instance.playerInput;
        playerAnimator = GetComponent<PlayerAnimator>();
        animator = GetComponent<Animator>();
    }
}
