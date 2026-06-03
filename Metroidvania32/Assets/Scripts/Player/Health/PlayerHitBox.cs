using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player hit box triggered by: " + collision.gameObject.name);
        // Implement damage logic here, e.g.:
        // PlayerHealth playerHealth = GetComponentInParent<PlayerHealth>();
        // if (playerHealth != null)
        // {
        //     playerHealth.TakeDamage(damageAmount);
        // }
    }

}
