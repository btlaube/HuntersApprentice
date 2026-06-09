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

        PlayerDataManager.Instance.OnHealthChanged += UpdateHealthUI;

        UpdateHealthUI(
            PlayerDataManager.Instance.Data.currentHealth,
            PlayerDataManager.Instance.Data.maxHealth // or expose MaxHealth property
        );
        Debug.Log($"Set player health ui to {PlayerDataManager.Instance.Data.currentHealth}");
    }

    private void UpdateHealthUI(float current, float max)
    {
        // healthFill.fillAmount = (float)current / max;
        healthBar.UpdateFillBar(current, max);
    }

    private void OnDestroy()
    {
        if (PlayerDataManager.Instance != null)
        {
            PlayerDataManager.Instance.OnHealthChanged -= UpdateHealthUI;
        }
    }
}