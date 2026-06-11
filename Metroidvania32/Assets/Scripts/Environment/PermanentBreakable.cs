using System.Collections;
using UnityEngine;

public class PermanentBreakable : MonoBehaviour, IDamageable
{
    [SerializeField] private string worldID;
    [SerializeField] private float health = 3f;

    [Header("Wiggle")]
    [SerializeField] private float wiggleDuration = 0.1f;
    [SerializeField] private float wiggleRotation = 10f;
    [SerializeField] private float wiggleScale = 1.1f;

    private Coroutine wiggleCoroutine;

    void Start()
    {
        if (WorldDataManager.Instance.currentWorldData.destroyedBreakables.Contains(worldID))
            Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        SpriteWiggle();

        health -= damage;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // WorldDataManager.Instance.currentWorldData.destroyedBreakables.Add(worldID);
        WorldDataManager.Instance.OnDestroyedPermanentBreakable(worldID);
        Destroy(gameObject);
    }

    public void SpriteWiggle()
    {
        if (wiggleCoroutine != null)
            StopCoroutine(wiggleCoroutine);

        wiggleCoroutine = StartCoroutine(WiggleCoroutine());
    }

    private IEnumerator WiggleCoroutine()
    {
        Vector3 originalScale = transform.localScale;
        Quaternion originalRotation = transform.localRotation;

        transform.localScale = originalScale * wiggleScale;
        transform.localRotation = Quaternion.Euler(0, 0, wiggleRotation);

        yield return new WaitForSeconds(wiggleDuration * 0.5f);

        transform.localRotation = Quaternion.Euler(0, 0, -wiggleRotation);

        yield return new WaitForSeconds(wiggleDuration * 0.5f);

        transform.localScale = originalScale;
        transform.localRotation = originalRotation;

        wiggleCoroutine = null;
    }
}