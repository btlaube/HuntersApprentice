using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaseGameData baseData;

    [SerializeField] private SaveManager saveManager;
    public PlayerStateManager playerStateManager;
    public WorldStateManager worldStateManager;
    public UIManager uiManager;
    
    // public bool GamePlayEnabled => currentState == GameState.GamePlay;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // UIManager.Instance.InitializeUIManagers();
        // currentState = GameState.MainMenu;
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            player = playerStateManager.GetSaveData(),
            world = worldStateManager.GetSaveData()
        };

        saveManager.SaveGame(saveData);
    }

    public void LoadGame()
    {
        SaveData saveData = saveManager.LoadGame();

        if (saveData == null)
            return;

        playerStateManager.ApplySaveData(saveData.player);
        worldStateManager.ApplySaveData(saveData.world);
        //TODO: Add Inventory, Map, and other Managers as needed
    }

    public void NewGame()
    {
        playerStateManager.InitializeNewRun(baseData);
        worldStateManager.InitializeNewRun(baseData);

        SaveGame();
        SaveCheckpoint();
        SceneLoadPlayerSpawner.instance.UpdatePlayerSpawnPointAndLoadScene(playerStateManager.CurrentState.spawnPointData);
    }



    // TODO: Add code for saving and loading checkpoints
    // public void ActivateCheckpoint(SpawnPointData spawnData)
    // {
            // SceneTransitionManager.SceneLoadPlayerSpawner.UpdatePlayerSpawnPoint(spawnData);
            // RunManager.Instance.SaveCheckpoint();
            
    // }

    // TODO: Subscribe to player death event
    // Load checkpoint in RunManager

    public void SaveCheckpoint()
    {
        playerStateManager.SaveCheckpointState();
        // worldStateManager.SaveCheckpointState();
    }

    public void Respawn()
    {
        playerStateManager.RestoreCheckpointState();
        // worldStateManager.RestoreCheckpointState();
        LevelLoader.instance.LoadScene(playerStateManager.CurrentState.spawnPointData.sceneName);
    }

    // public void SwitchState(GameState newState)
    // {
    //     currentState = newState;
    //     // uiManager.ApplySceneUI(newState);
    // }

    // private bool isPaused;
    // public void ToggleSettings()
    // {
    //     // pause and open settings menu or unpause and close settings menu
    //     if (isPaused)
    //     {
    //         // close settings menu and unpause
    //         isPaused = false;
    //         UIManager.Instance.ToggleSettings();
    //     }
    //     else
    //     {
    //         // open settings menu and pause
    //         isPaused = true;
    //     }

    // }

}