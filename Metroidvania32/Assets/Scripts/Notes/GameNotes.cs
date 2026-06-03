// Game Notes



// All Systems

// PersistingSystemsRoot
    // Only instance of DontDestroyOnLoad
        // All other persisting systems exist as children of this GameObject

// SaveManager
    // Serializes and deserializes game data to and from disk

// GameManager

//GameDataManager
    // PlayerDataManager
        // PlayerData
    // WorldDataManager
        // WorldData
    // MapDataManager
        // MapData


// GameStateManager
    // Game States
        // Menu
        // Gameplay
        // Cutscene

// PlayerRuntimeDataManager
    // PlayerRuntimeData
        // PlayerHealth
        // PlayerCurrency
    // public void SaveCheckpoint()
    // public void RestoreCheckpoint()

// PlayerDataManager
    // PlayerData
        // MaxHealth
        // CurrentHealth
        // Inventory
        // Abilities
        // Position
        // CurrentSpawn
            // CurrentRoom
            // SpawnPoint (spawnID)
        // Currency
    // public void InitializeNewRun(BaseGameData baseData)
    // public PlayerSaveData GetSaveData()
    // public void ApplySaveData(PlayerSaveData saveData)

// WorldDataManager
    // WorldData
        // WorldData
        // DefeatedBosses
        // OpenedDoors
        // ActivatedLevers
        // CollectedItems
        // AcquiredAbilities
        // TriggeredCutscenes
        // DestroyedObjects
        // NPCStates
    // public void InitializeNewRun(BaseGameData baseData)
    // public WorldSaveData GetSaveData()
    // public void ApplySaveData(WorldSaveData saveData)

// MapDataManager
    // MapData
        // AcquiredAreaMaps
        // VisitedRooms
        // RevealedRooms
        // MapPins
        // CustomMarkers
    // public void InitializeNewRun(BaseGameData baseData)
    // public MapSaveData GetSaveData()
    // public void ApplySaveData(MapSaveData saveData)

// Map system
    // Full completed map
        // Individual "Area Maps"
            // Individual "Room Maps" for each scene within one "Area Map"
    // GameMap
        // Stores which AreaMaps are in player inventory
        // AreaMap
            // Stores which RoomMaps have been marked as entered by the player
            // RoomMap
                // Stores whether or not the player has entered the room before
// DialogueManager
// InventoryManager


// MasterInputHandler
    // PlayerInputHandler
        // Sets variables based on player input, such as movement direction and action triggers
    // UIInputHandler
        // Calls functions on UIManager based on player input, such as opening the inventory, map, or pausing the game

// UIManager
    // Manages the UI elements of the game, such as health bars, inventory screens, and dialogue boxes
    // Receives input from UIInputHandler and checks the current game state from GameStateManager to determine if UI actions should be performed
    // Sends UI state changes from UIInputHandler to UIStateManager to manage which UI panels are active
    // Opens correct UI based on UIStateManager's current state


// UIStateManager
    // Manages the state of the UI, such as which panels are active    
    // Determines which UI can transition to other UI states
    // Transitions between UI menu states
    // States
        // StClosed
        // StMainMenu
            // Can transition to Options
            // Can transition to SaveSelect
        // StOptions
            // Can transition to MainMenu
        // StSaveSelect
            // SaveSelectUIState
                // StSelectingSave
                // StDeletingSave
                // StConfirmingDelete
                // StOverwritingSave
                // StConfirmingOverwrite
        // StInventory
            // Can transition to StMap
        // StMap
            // Can transition to StInventory
        // StDialogue
        // StPause
            // Can transition to StOptions
            // 