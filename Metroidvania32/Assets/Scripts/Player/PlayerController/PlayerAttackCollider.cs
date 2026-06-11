using UnityEngine;
using System.Collections.Generic;

public class PlayerAttackCollider : MonoBehaviour
{
    [SerializeField] private float damageAmount = 1f;

    private HashSet<IDamageable> hitTargets = new();
    private HashSet<IEffectable> effectedTargets = new();
    public string elementID;

    private void OnEnable()
    {
        hitTargets.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null && !hitTargets.Contains(damageable))
        {
            hitTargets.Add(damageable);
            damageable.TakeDamage(damageAmount);
        }

        IEffectable effectable = other.GetComponent<IEffectable>();

        if (effectable != null && !effectedTargets.Contains(effectable))
        {
            effectedTargets.Add(effectable);
            effectable.ApplyEffect(elementID);
        }
    }

    public void SetAttackElement(string element)
    {
        elementID = element;
    }

}