using UnityEngine;

public class EnemyEffects : MonoBehaviour, IEffectable
{
    // [SerializeField] private float health = 3f;

    public void ApplyEffect(string effectID)
    {
        Debug.Log($"Applying effect: {effectID}");
        switch (effectID)
        {
           case "fire":
                ApplyBurn();
                break;
            case "ice":
                ApplyFreeze();
                break;
            case "wind":
                ApplyKnockback();
                break;
            case "electricity":
                ApplyShock();
                break;
            default:
                break;
        }
    }

    private void ApplyBurn()
    {
        Debug.Log("Applied burn!");
    }

    private void ApplyFreeze()
    {
        Debug.Log("Applied freeze!");
        
    }
    private void ApplyKnockback()
    {
        Debug.Log("Applied knockback!");

    }
    private void ApplyShock()
    {
        Debug.Log("Applied shock!");

    }

}