using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 3f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log($"Enemy took {damage} damage");

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}