using UnityEngine;

public class PauseUI : CanvasController
{
    [SerializeField] private GameplayUIManager gameplayUIManager;
    [SerializeField] private GameObject confirmQuitPanel;

    void Start()
    {
        HideConfirmQuitPanel();
    }

    public void ShowConfirmQuitPanel()
    {
        confirmQuitPanel.SetActive(true);
    }

    public void HideConfirmQuitPanel()
    {
        confirmQuitPanel.SetActive(false);
    }

    public void OnSaveAndQuit()
    {
        ShowConfirmQuitPanel();
    }

    public void OnQuitConfirmed()
    {
        GameManager.Instance.OnSaveAndExit();
    }

}
