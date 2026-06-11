using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initiate(int direction)
    {
        Vector3 currentVelocity = rb.linearVelocity;
        currentVelocity.x = speed * direction;
        rb.linearVelocity = currentVelocity;
    }
}
