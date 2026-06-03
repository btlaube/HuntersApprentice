using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private MapUI mapUI;
    [SerializeField] private PauseUI pauseUI;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private MainMenuUI mainMenuUI;
    // [SerializeField] private JournalUI journalUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private SaveSelectUI saveSelectUI;

    // private UIInputHandler uiInput;

    private bool gameplayVisible;
    private bool dialogueOpen;
    private bool pauseOpen;
    private bool inventoryOpen;


    public static UIManager Instance { get; private set; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ApplySceneUI();
    }

    public void InitializeUIManagers()
    {
        hudManager.InitializeHUD();
        // Debug.Log($"Initialized HUD Manager with health: {GameManager.Instance.playerStateManager.CurrentState.currentHealth}");
    }

    private void ApplySceneUI()
    {
        switch (GameStateManager.Instance.CurrentState)
        {
            case GameState.MainMenu:
                // gameplayVisible = false;
                // dialogueOpen = false;
                // pauseOpen = false;
                // inventoryOpen = false;
                hudManager.Hide();
                inventoryUI.Hide();
                mapUI.Hide();
                pauseUI.Hide();
                dialogueUI.Hide();
                mainMenuUI.Show();
                
                break;

            case GameState.Gameplay:
                // gameplayVisible = true;
                // dialogueOpen = false;
                // pauseOpen = false;
                // inventoryOpen = false;
                hudManager.Show();
                // hudManager.InitializeHUD();

                // inventoryUI.Show();
                // mapUI.Show();
                // pauseUI.Show();
                // dialogueUI.Show();
                mainMenuUI.Hide();
                break;

            case GameState.Cutscene:
                // gameplayVisible = false; 
                // dialogueOpen = true; // assuming cutscenes use dialogue UI
                // pauseOpen = false;
                // inventoryOpen = false;
                hudManager.Hide();
                inventoryUI.Hide();
                mapUI.Hide();
                pauseUI.Hide();
                dialogueUI.Show();
                mainMenuUI.Hide();
                break;

            default:
                // gameplayVisible = true;
                // dialogueOpen = false;
                // pauseOpen = false;
                // inventoryOpen = false;
                hudManager.Hide();
                inventoryUI.Hide();
                mapUI.Hide();
                pauseUI.Hide();
                dialogueUI.Hide();
                mainMenuUI.Show();
                break;
        }

        // RefreshUI();
    }

    private void RefreshUI()
    {
        hudManager.SetVisible(gameplayVisible);

        dialogueUI.SetVisible(dialogueOpen);

        pauseUI.SetVisible(pauseOpen);

        inventoryUI.SetVisible(inventoryOpen);

        mainMenuUI.SetVisible(!gameplayVisible);
    }

    public void OnInventoryPressed()
    {
        if (!CanOpenMenus())
            return;

        UIStateManager.Instance.Open(UIState.Inventory);
    }

    public void OnMapPressed()
    {
        if (!CanOpenMenus())
            return;

        UIStateManager.Instance.Open(UIState.Map);
    }

    public void OnJournalPressed()
    {
        if (!CanOpenMenus())
            return;

        UIStateManager.Instance.Open(UIState.Journal);
    }

    public void OnPausePressed()
    {
        if (!CanOpenMenus())
            return;

        UIStateManager.Instance.Open(UIState.Pause);
    }

    public void OnSettingsPressed()
    {
        if (!CanOpenMenus())
            return;

        UIStateManager.Instance.Open(UIState.Settings);
    }

    public void OnSaveSelectPressed()
    {
        // if (!CanOpenMenus())
            // return;

        UIStateManager.Instance.Open(UIState.SaveSelect);
    }

    private bool CanOpenMenus()
    {
        GameState state = GameStateManager.Instance.CurrentState;

        return state != GameState.MainMenu
            && state != GameState.PlayerDead
            && state != GameState.Respawning;
    }

    public void OpenPause()
    {
        pauseUI.Show();
    }

    public void OpenInventory()
    {
        inventoryUI.Show();
    }

    public void OpenMap()
    {
        mapUI.Show();
    }

    public void OpenJournal()
    {
        // journalUI.Show();
    }

    public void OpenSettings()
    {
        settingsUI.Show();
    }

    public void OpenSaveSelect()
    {
        saveSelectUI.Show();
    }

    public void ClosePause()
    {
        pauseUI.Hide();
    }

    public void CloseInventory()
    {
        inventoryUI.Hide();
    }

    public void CloseMap()
    {
        mapUI.Hide();
    }

    public void CloseJournal()
    {
        // journalUI.Hide();
    }

    public void CloseSettings()
    {
        settingsUI.Hide();
    }

    public void CloseSaveSelect()
    {
        saveSelectUI.Hide();
    }

}
