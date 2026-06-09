using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OnBootSequence()
    {
        // Tell GameStateManager to change to loading state
        GameStateManager.Instance.ChangeState(GameState.Loading);
        // Tell LevelLoader to load the "MainMenu" scene
        LevelLoader.Instance.LoadScene("MainMenu");
    }

    public void OnNewGame()
    {
        int slot = SaveManager.Instance.CreateNewSave();
        SaveManager.Instance.LoadGame(slot);
        // Tell GameDataManager to initialize Sub Managers with base game data
        // GameDataManager.Instance.InitializeNewRun();
        // // Get newly loaded base data from GameDataManager
        // // GameData gameData = GameDataManager.Instance.GetGameData();
        // // Tell SaveManager to save the new data
        // SaveManager.Instance.SaveGame();
        // // Save the first checkpoint, setting checkpointSnapshot.currency to 0
        CheckpointManager.Instance.SaveCheckpoint();
        // // Tell SpawnManager to update the Player Spawn Location
        SpawnManager.Instance.SetSpawnLocation(PlayerDataManager.Instance.Data.spawnPointData);
        // // Tell GameStateManager to change to loading state
        GameStateManager.Instance.ChangeState(GameState.Loading);
        // // Tell LevelLoader to load the current spawn location in SpawnManager
        LevelLoader.Instance.LoadNextScene();
    }

    public void OnLoadGame(int slot)
    {
        // Get saveData from the SaveManager, which deserializes data
        // SaveData saveData = SaveManager.Instance.LoadGame(slot);
        // Tell GameDataManager to update current data based on loaded data
        // GameDataManager.Instance.SetGameData(saveData);
        SaveManager.Instance.LoadGame(slot);
        // Tell CheckpointManager to set the current checkpoint snapshot currency to loaded currency amount
        CheckpointManager.Instance.SaveCheckpoint();
        // Tell SpawnManager to update the Player Spawn Location
        SpawnManager.Instance.SetSpawnLocation(PlayerDataManager.Instance.Data.spawnPointData);
        // Tell GameStateManager to change to loading state
        GameStateManager.Instance.ChangeState(GameState.Loading);
        // Tell LevelLoader to load the current spawn location in SpawnManager
        LevelLoader.Instance.LoadNextScene();
    }

    public void OnSaveAndExit()
    {
        SaveGame();
        // Tell GameStateManager to change to loading state
        GameStateManager.Instance.ChangeState(GameState.Loading);
        // Tell LevelLoader to load the "MainMenu" scene
        LevelLoader.Instance.LoadScene("MainMenu");
    }

    public void SaveGame()
    {
        // Get data from GameDataManager
        // SaveData gameData = GameDataManager.Instance.GetGameData();
        // // Tell SaveManager to save the retreived data
        SaveManager.Instance.SaveGame();
    }

    public void OnPlayerDeath(Vector2 playerDeathPosition)
    {
        // Tell CheckpointManager to reset player currency to checkpoint state and spawn checkpointObject
        CheckpointManager.Instance.OnPlayerDeath(playerDeathPosition);
        // Tell GameStateManager to change to playerDead state
        GameStateManager.Instance.ChangeState(GameState.PlayerDead);
        // Tell SpawnManager to update the Player Spawn Location from the spawn data in PlayerDataManager which is updated on NewGame, LoadGame, and when interacting with SavePoints
        SpawnManager.Instance.SetSpawnLocation(PlayerDataManager.Instance.Data.spawnPointData);
        // Tell GameStateManager to change to loading state
        GameStateManager.Instance.ChangeState(GameState.Loading);
        // Tell LevelLoader to load the current spawn location in SpawnManager
        LevelLoader.Instance.LoadNextScene();
    }

    public void OnSceneTransitionTrigger(SpawnPointData spawnData)
    {
        // Tell SpawnManager to set the current spawn location to the spawn data from the transition trigger
        SpawnManager.Instance.SetSpawnLocation(spawnData);
        // Tell GameStateManager to change to loading state
        GameStateManager.Instance.ChangeState(GameState.Loading);
        // Tell LevelLoader to load the current spawn location in SpawnManager
        LevelLoader.Instance.LoadNextScene();
    }

    public void OnSavePoint(SpawnPointData spawnData)
    {
        // Fully heal the player
        PlayerDataManager.Instance.SetHealth(PlayerDataManager.Instance.Data.maxHealth);
        // Tell CheckpointManager to update checkpoint snapshot (overwriting previous checkpoint)
        CheckpointManager.Instance.SaveCheckpoint();
        // Tell the PlayerDataManager to update the saved player spawn data
        PlayerDataManager.Instance.Data.spawnPointData = spawnData;
        // Tell the spawn manager to update the current spawn data
        SpawnManager.Instance.SetSpawnLocation(spawnData);
        // Save current game data to disk
        SaveGame();
    }

}