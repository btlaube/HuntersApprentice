using UnityEngine;

public class HazardCollider : MonoBehaviour
{
    [SerializeField] private float damageAmount = 9999f; // Amount of damage to deal to the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Instantly kill the player
            }
        }
    }
}
