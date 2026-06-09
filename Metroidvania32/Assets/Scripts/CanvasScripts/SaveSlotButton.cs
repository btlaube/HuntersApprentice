using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.EventSystems;

public class SaveSlotButton : MonoBehaviour
{
    [SerializeField] private Button button;
    public TMP_Text currentLocationText;
    [SerializeField] private GameObject loadOrDeletePanel;
    [SerializeField] private GameObject firstSelected;

    // public Action<int> OnSelected;
    public Action<int> OnLoaded;
    public Action<int> OnDeleted;
    public Action<int> OnCanceled;
    private Metadata metadata;

    void Start()
    {
        HideLoadOrDelete();
    }

    public void Initialize(Metadata metadata)
    {
        this.metadata = metadata;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        // show load or delete options
        ShowLoadOrDelete();
    }

    public void ShowLoadOrDelete()
    {
        loadOrDeletePanel.SetActive(true);
        SetSelectedObject();
    }

    public void HideLoadOrDelete()
    {
        loadOrDeletePanel.SetActive(false);
    }

    private void SetSelectedObject()
    {
        StartCoroutine(SetFirstSelectedWithDelay(firstSelected));
    }

    public virtual IEnumerator SetFirstSelectedWithDelay(GameObject obj)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        // EventSystem.current.SetSelectedGameObject(obj);
        obj.GetComponent<Button>().Select();
    }

    public void OnSaveSlotLoad()
    {
        OnLoaded?.Invoke(metadata.slotNumber);
    }

    public void OnSaveSlotDelete()
    {
        OnDeleted?.Invoke(metadata.slotNumber);
    }

    public void OnSaveSlotCancel()
    {
        HideLoadOrDelete();
        OnCanceled?.Invoke(metadata.slotNumber);
    }

}