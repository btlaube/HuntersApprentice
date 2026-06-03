using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    // public PlayerStateManager stateManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        // stateManager = GetComponent<PlayerStateManager>();
    }
}
