using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ColliderEventTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (onTriggerEnter != null) onTriggerEnter.Invoke();
    }
}
