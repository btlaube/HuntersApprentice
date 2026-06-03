using UnityEngine;

public enum UIState
{
    None,
    MainMenu,
    SaveSelect,
    Controls,

    Settings,
    Pause,
    Inventory,
    Map,
    Journal
}
public class UIStateManager : MonoBehaviour
{
    public static UIStateManager Instance;

    public UIState CurrentState;// { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Open(UIState state)
    {
        if (CurrentState == state)
        {
            CloseCurrent();
            return;
        }

        CloseCurrent();

        CurrentState = state;

        OpenPanel(state);

        if (GameStateManager.Instance.CurrentState == GameState.Gameplay)
        {
            GameStateManager.Instance.ChangeState(GameState.Menu);
        }
    }

    public void CloseCurrent()
    {
        if (CurrentState == UIState.None)
            return;

        ClosePanel(CurrentState);

        CurrentState = UIState.None;

        if (GameStateManager.Instance.CurrentState == GameState.Menu)
        {
            GameStateManager.Instance.ChangeState(GameState.Gameplay);
        }
    }

    private void OpenPanel(UIState state)
    {
        // activate panel
        switch (state)
        {
            case UIState.Settings:
                // Activate Settings Panel
                UIManager.Instance.OpenSettings();
                break;
            case UIState.Inventory:
                // Activate Inventory Panel
                UIManager.Instance.OpenInventory();
                break;
            case UIState.Map:
                // Activate Map Panel
                UIManager.Instance.OpenMap();
                break;
            case UIState.Journal:
                // Activate Journal Panel
                UIManager.Instance.OpenJournal();
                break;
            case UIState.Pause:
                // Activate Pause Panel
                UIManager.Instance.OpenPause();
                break;
            case UIState.SaveSelect:
                // Activate Save Select Panel
                UIManager.Instance.OpenSaveSelect();
                break;
        }
    }

    private void ClosePanel(UIState state)
    {
        // deactivate panel
        switch (state)
        {
            case UIState.Settings:
                // Deactivate Settings Panel
                UIManager.Instance.CloseSettings();
                break;
            case UIState.Inventory:
                // Deactivate Inventory Panel
                UIManager.Instance.CloseInventory();
                break;
            case UIState.Map:
                // Deactivate Map Panel
                UIManager.Instance.CloseMap();
                break;
            case UIState.Journal:
                // Deactivate Journal Panel
                UIManager.Instance.CloseJournal();
                break;
            case UIState.Pause:
                // Deactivate Pause Panel
                UIManager.Instance.ClosePause();
                break;
            case UIState.SaveSelect:
                // Deactivate Save Select Panel
                UIManager.Instance.CloseSaveSelect();
                break;
        }
    }
}
