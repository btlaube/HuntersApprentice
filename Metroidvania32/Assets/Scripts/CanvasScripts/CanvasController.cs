using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;

    private Canvas canvas;
    public virtual void SetVisible(bool visible)
    {
        if (visible)
            Show();
        else
            Hide();
    }

    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        // canvas.enabled = true;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        // canvas.enabled = false;
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
