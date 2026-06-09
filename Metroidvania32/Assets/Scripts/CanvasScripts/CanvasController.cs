using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;
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

    public virtual void SetFirstSelected()
    {
        StartCoroutine(SetFirstSelectedWithDelay(firstSelected));
    }

    public virtual IEnumerator SetFirstSelectedWithDelay(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(obj);
    }

}
