using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private SpawnPointData spawnData;

    void Start()
    {
        HideInteractIcon();
    }

    public void Interact()
    {
        ActivateSavePoint();
    }
    public void ShowInteractIcon()
    {
        sr.enabled = true;
    }
    public void HideInteractIcon()
    {
        sr.enabled = false;
    }

    public void ActivateSavePoint()
    {
        GameManager.Instance.OnSavePoint(spawnData);
    }
}
