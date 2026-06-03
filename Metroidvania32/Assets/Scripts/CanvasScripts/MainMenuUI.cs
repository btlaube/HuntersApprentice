using UnityEngine;

public class MainMenuUI : CanvasController
{
    public void OnNewGame()
    {
        // RunManager.Instance.InitializeNewRun();
        // LevelLoader.instance.LoadScene(1);
        GameManager.Instance.NewGame();
    }

    // TODO: Implement Load Game functionality
    // TODO: Implement Options Menu functionality

    public void OnLoadGame()
    {
        UIManager.Instance.OnSaveSelectPressed();
    }
    

}
