using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenuUI : CanvasController
{
    // public void OnNewGame()
    // {
    //     // GameManager.Instance.OnNewGame();
    //     UIStateManager.Instance.Open(UIState.SaveSelect);
    //     // UIStateManager.Instance.Open(UIState.SaveSelect);
    // }

    // // TODO: Implement Load Game functionality
    // // TODO: Implement Options Menu functionality

    // public void OnLoadGame()
    // {
    //     UIManager.Instance.OnSaveSelectPressed();
    // }
    // [SerializeField] private GameObject firstSelected;
    

    [SerializeField] private MainMenuUIManager mainMenuUIManager;

    public void OnStartGameButton()
    {
        mainMenuUIManager.OpenSaveSelect();
    }

    public void OnOptionsButton()
    {
        mainMenuUIManager.OpenOptions();
    }

    // public void SetFirstSelected()
    // {
    //     StartCoroutine(SetFirstSelectedWithDelay(firstSelected));
    // }

    // public IEnumerator SetFirstSelectedWithDelay(GameObject obj)
    // {
    //     yield return new WaitForEndOfFrame();
    //     EventSystem.current.SetSelectedGameObject(obj);
    // }

}
