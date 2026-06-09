using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{

    [SerializeField] private CanvasController mainMenuUI;
    [SerializeField] private CanvasController saveSelectUI;
    [SerializeField] private CanvasController optionsUI;    

    void Start()
    {
        OpenMainMenu();
        CloseSaveSelect();
        CloseOptions();
    }

    public void OpenMainMenu()
    {
        ShowCanvas(mainMenuUI);
        mainMenuUI.SetFirstSelected();
    }

    public void OpenSaveSelect()
    {
        HideCanvas(mainMenuUI);
        ShowCanvas(saveSelectUI);
        saveSelectUI.SetFirstSelected();
    }

    public void OpenOptions()
    {
        HideCanvas(mainMenuUI);
        ShowCanvas(optionsUI);
        optionsUI.SetFirstSelected();
    }

    public void CloseSaveSelect()
    {
        HideCanvas(saveSelectUI);
        OpenMainMenu();
    }

    public void CloseOptions()
    {
        HideCanvas(optionsUI);
        OpenMainMenu();
    }

    public void ShowCanvas(CanvasController canvas)
    {
        canvas.Show();
    }

    public void HideCanvas(CanvasController canvas)
    {
        canvas.Hide();
    }


}
