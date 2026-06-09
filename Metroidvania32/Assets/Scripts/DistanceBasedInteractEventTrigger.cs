using UnityEngine;
using UnityEngine.Events;

public class DistanceBasedInteractEventTrigger : MonoBehaviour
{
    public UnityEvent OnInteract;

    [SerializeField] private SpriteRenderer sr;

    private void Awake()
    {
        if (sr != null)
        {
            sr.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D context)
    {
        if (context.CompareTag("Player"))
        {
            sr.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D context)
    {
        if (context.CompareTag("Player"))
        {
            sr.enabled = false;
        }
    }
}