using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerComponents playerComponents;

    [Header("Melee Attack")]
    [SerializeField] private Transform meleeAttackSpawnPoint;
    [SerializeField] private float meleeAttackDuration = 0.5f;
    [SerializeField] private GameObject meleeAttackPrefab;

    [SerializeField] private bool meleeAttackConsumed;
    [SerializeField] private float meleeAttackCooldown;
    [SerializeField] private float meleeAttackCooldownTimer;

    [Header("Ranged Attack")]
    [SerializeField] private Transform rangedAttackSpawnPoint;
    // [SerializeField] private float meleeAttackDuration = 0.5f;
    [SerializeField] private GameObject rangedAttackPrefab;

    [SerializeField] private bool rangedAttackConsumed;
    [SerializeField] private float rangedAttackCooldown;
    [SerializeField] private float rangedAttackCooldownTimer;
    
    private PlayerInputHandler input;

    void Start()
    {
        input = MasterInputHandler.Instance.playerInput;
        meleeAttackConsumed = false;
        meleeAttackCooldownTimer = 0f;
        rangedAttackConsumed = false;
        rangedAttackCooldownTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (!input.inputEnabled) return;
        if (input == null)
            return;
        if (InventoryDataManager.Instance.currentInventoryData.unlockedUpgrades.Contains("melee")) HandleMeleeAttack();
        if (InventoryDataManager.Instance.currentInventoryData.unlockedUpgrades.Contains("ranged")) HandleRangedAttack();
    }

    private void HandleMeleeAttack()
    {
        // Tick cooldown down
        if (meleeAttackCooldownTimer > 0f)
        {
            meleeAttackCooldownTimer -= Time.fixedDeltaTime;
        }

        // Attack pressed
        if (input.isMeleeAttacking)
        {
            // Only allow attack if:
            // - not already consumed
            // - cooldown finished
            if (!meleeAttackConsumed && meleeAttackCooldownTimer <= 0f)
            {
                // Debug.Log("Player is attacking!");
                playerComponents.animator.SetTrigger("Melee");
                StartCoroutine(MeleeAttackCoroutine());

                meleeAttackConsumed = true;
            }
        }
        // Attack released
        else
        {
            // Only start cooldown if an attack actually happened
            if (meleeAttackConsumed)
            {
                meleeAttackCooldownTimer = meleeAttackCooldown;
            }

            // Allow next attack after release
            meleeAttackConsumed = false;
        }
    }

    private IEnumerator MeleeAttackCoroutine()
    {
        // Spawn attack prefab
        GameObject attackInstance = Instantiate(meleeAttackPrefab, meleeAttackSpawnPoint.position, Quaternion.identity);
        // Parent the attack to the player so it moves with them
        attackInstance.transform.parent = transform;
        if (InventoryDataManager.Instance
            .TryGetEquippedElement("melee", out var element))
        {
            attackInstance
                .GetComponent<PlayerAttackCollider>()
                .SetAttackElement(element);
        }
        // Lock player turning
        GetComponent<PlayerMovement>().DisableFacing();

        // Wait for the duration of the attack
        yield return new WaitForSeconds(meleeAttackDuration);

        // Unlock player turning
        GetComponent<PlayerMovement>().EnableFacing();
        // Destroy the attack instance after the duration
        Destroy(attackInstance);
    }

    private void HandleRangedAttack()
    {
        // Tick cooldown down
        if (rangedAttackCooldownTimer > 0f)
        {
            rangedAttackCooldownTimer -= Time.fixedDeltaTime;
        }

        // Attack pressed
        if (input.isRangedAttacking)
        {
            // Only allow attack if:
            // - not already consumed
            // - cooldown finished
            if (!rangedAttackConsumed && rangedAttackCooldownTimer <= 0f)
            {
                // Debug.Log("Player is attacking!");
                playerComponents.animator.SetTrigger("Ranged");
                StartCoroutine(RangedAttackCoroutine());

                rangedAttackConsumed = true;
            }
        }
        // Attack released
        else
        {
            // Only start cooldown if an attack actually happened
            if (rangedAttackConsumed)
            {
                rangedAttackCooldownTimer = rangedAttackCooldown;
            }

            // Allow next attack after release
            rangedAttackConsumed = false;
        }
    }

    private IEnumerator RangedAttackCoroutine()
    {
        // Spawn attack prefab
        GameObject attackInstance = Instantiate(rangedAttackPrefab, rangedAttackSpawnPoint.position, Quaternion.identity);
        // Parent the attack to the player so it moves with them
        // attackInstance.transform.parent = transform;
        attackInstance.GetComponent<RangedAttack>().Initiate(playerComponents.FacingDirection);
        if (InventoryDataManager.Instance
            .TryGetEquippedElement("ranged", out var element))
        {
            attackInstance
                .GetComponent<PlayerAttackCollider>()
                .SetAttackElement(element);
        }
        // Lock player turning
        GetComponent<PlayerMovement>().DisableFacing();

        // Wait for the duration of the attack
        yield return null;//new WaitForSeconds(meleeAttackDuration);

        // Unlock player turning
        GetComponent<PlayerMovement>().EnableFacing();
    }

}
