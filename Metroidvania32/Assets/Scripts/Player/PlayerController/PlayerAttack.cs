using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInputHandler input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Transform attackSpawnPoint;
    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private GameObject attackPrefab;

    [SerializeField] private bool attackConsumed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackCooldownTimer;

    void Start()
    {
        input = MasterInputHandler.Instance.playerInput;
        attackConsumed = false;
        attackCooldownTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (!input.inputEnabled) return;
        if (input == null)
            return;

        HandleAttack();
    }

    private void HandleAttack()
    {
        // Tick cooldown down
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.fixedDeltaTime;
        }

        // Attack pressed
        if (input.isAttacking)
        {
            // Only allow attack if:
            // - not already consumed
            // - cooldown finished
            if (!attackConsumed && attackCooldownTimer <= 0f)
            {
                Debug.Log("Player is attacking!");
                StartCoroutine(AttackCoroutine());

                attackConsumed = true;
            }
        }
        // Attack released
        else
        {
            // Only start cooldown if an attack actually happened
            if (attackConsumed)
            {
                attackCooldownTimer = attackCooldown;
            }

            // Allow next attack after release
            attackConsumed = false;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        // Spawn attack prefab
        GameObject attackInstance = Instantiate(attackPrefab, attackSpawnPoint.position, Quaternion.identity);
        // Parent the attack to the player so it moves with them
        attackInstance.transform.parent = transform;
        // Lock player turning
        GetComponent<PlayerMovement>().DisableFacing();


        // Wait for the duration of the attack
        yield return new WaitForSeconds(attackDuration);

        // Unlock player turning
        GetComponent<PlayerMovement>().EnableFacing();
        // Destroy the attack instance after the duration
        Destroy(attackInstance);
    }

}
