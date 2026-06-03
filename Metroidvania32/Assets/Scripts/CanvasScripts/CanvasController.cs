using UnityEngine;

public abstract class CanvasController : MonoBehaviour
{
    public virtual void SetVisible(bool visible)
    {
        if (visible)
            Show();
        else
            Hide();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
