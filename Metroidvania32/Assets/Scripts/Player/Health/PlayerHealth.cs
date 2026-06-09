using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    // [SerializeField] private PlayerStateManager playerStateManager;

    public float currentHealth;
    public float maxHealth;

    // public event Action<float, float> OnHealthChanged;

    void Start()
    {
        currentHealth = PlayerDataManager.Instance.Data.currentHealth;
        maxHealth = PlayerDataManager.Instance.Data.maxHealth;

        // OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // playerStateManager.CurrentState.currentHealth = currentHealth;

        // OnHealthChanged?.Invoke(currentHealth, maxHealth);
        PlayerDataManager.Instance.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Implement respawn or game over logic here
        GetComponent<PlayerDeath>().OnPlayerDeath();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        // playerStateManager.CurrentState.currentHealth = currentHealth;
        // OnHealthChanged?.Invoke(currentHealth, maxHealth);
        PlayerDataManager.Instance.SetHealth(currentHealth);
    }

}
