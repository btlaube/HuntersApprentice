using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    // [SerializeField] private Image healthFill;
    [SerializeField] private FillBar healthBar;

    // private PlayerHealth playerHealth;

    public void Initialize()
    {
        // playerHealth = health;

        GameManager.Instance.playerStateManager.OnHealthChanged += UpdateHealthUI;

        UpdateHealthUI(
            GameManager.Instance.playerStateManager.CurrentState.currentHealth,
            GameManager.Instance.playerStateManager.CurrentState.maxHealth // or expose MaxHealth property
        );
        Debug.Log($"Set player health ui to {GameManager.Instance.playerStateManager.CurrentState.currentHealth}");
    }

    private void UpdateHealthUI(float current, float max)
    {
        // healthFill.fillAmount = (float)current / max;
        healthBar.UpdateFillBar(current, max);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.playerStateManager != null)
        {
            GameManager.Instance.playerStateManager.OnHealthChanged -= UpdateHealthUI;
        }
    }
}