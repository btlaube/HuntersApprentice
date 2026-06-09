using UnityEngine;

public abstract class CollectableWorldItem : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer sr;
    public string flagID;

    void Start()
    {
        HideInteractIcon();
        // if (WorldDataManager.currentWorldData.collectedItems.Contains(flagID)) Destroy(gameObject);
    }


    public abstract void Interact();

    public void ShowInteractIcon()
    {
        sr.enabled = true;
    }

    public void HideInteractIcon()
    {
        sr.enabled = false;
    }
}
