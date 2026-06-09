// Game Notes


// In Scene Hierarchy:
    // Bootstrap scene
        // First to load when game starts, contains only the PersistingSystemsRoot GameObject with all persisting systems as children
    // Menu scene
        // PersistingSystemsRoot (from bootstrap scene)
    // Gameplay scene
        // PersistingSystemsRoot (from bootstrap scene)
        // Possible Game scene objects:
            // Player
            // Enemies
            // Bosses
            // NPCs
            // Interactive objects (doors, levers, chests, etc.)
            // Environmental hazards (traps, spikes, etc.)
            // Collectibles (items, currency, etc.)


// Psuedocode
// public class PersistingSystemsRoot
    // public PersistingSystemsRoot Instance { get; private set;}
    // void Awake()
    // {
        // Set Instance
        // DontDestoryOnLoad()
    // }
    // void Start()
    // {
        // GameManager.Initialize()
        // UIManager.Initialize()
        // GameStateManager.Initialize()
        // Etc.
        // Load Main menu scene
    // }

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

// CheckpointManager
    // CheckpointData
        // PlayerHealth
        // PlayerCurrency
        // PlayerSpawnData
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


// Player Specific Systems
    // PlayerHealth
    // PlayerDeath
    // PlayerMovement
    // PlayerJump
    // PlayerGravity
    // PlayerDash
        // Dash types based on equipped effect:
            // Base dash (no effect)
            // Fire dash (leaves a trail of fire that damages enemies and can ignite certain objects in the environment)
            // Ice dash (leaves a trail of ice that slows enemies and can freeze certain objects in the environment)
            // Electric dash (leaves a trail of electricity that shocks enemies and can electrify certain objects in the environment)
            // Wind dash (creates a gust of wind that knocks back enemies and can activate certain objects in the environment)
    // PlayerCombat
        // PlayerMelee
            // Melee types based on equipped effect: 
                // Base melee attack (no effect)
                // Fire melee attack (burns enemies, can ignite certain objects in the environment)
                // Ice melee attack (slows enemies, can freeze certain objects in the environment)
                // Electric melee attack (shocks enemies, can electrify certain objects in the environment)
                // Wind melee attack (knocks back enemies, can activate certain objects in the environment)
        // PlayerRanged
            // Ranged attack types based on equipped effect:
                // Base ranged attack (no effect)
                // Fire ranged attack (burns enemies, can ignite certain objects in the environment)
                // Ice ranged attack (slows enemies, can freeze certain objects in the environment)
                // Electric ranged attack (shocks enemies, can electrify certain objects in the environment)
                // Wind ranged attack (knocks back enemies, can activate certain objects in the environment)
    // PlayerVelocity
    // PlayerAnimator
    // PlayerStateManager
    // PlayerCollisionHandler

// Enemy Specific Systems
    // EnemyHealth
    // EnemyDeath
    // EnemyMovement
    // EnemyCombat
    // EnemyAI
    // EnemyAnimator
    // EnemyStateManager
    // Interfaces
        // IFlammable, IFreezable, IShockable, IKnockbackable

// Interactable Environment
    // Breakables
        // Takes basic damage from the player
    // Flammables
        // Can have fire elemental effect applied
    // Freezables
        // Can have 
    // Shockables
        // Powerables
            // Can be activated by electricity elemental effects
    // Knockbackables


// Permanent Breakables
    // When broken, trigger specific flag for breakableID in WorldDataManager

// Boss
    // When defeatred, trigger specific flag for bossID in WorldDataManager

// Doors, switches, breakable walls, etc
    // When interacted with, trigger specific flag in WorldDataManager

// Elemental Effects System
    // Base elemental effects:
        // Fire
        // Ice
        // Electric
        // Wind

// IFlammable
    // Interface for objects that can be ignited by the fire elemental effect
    // Methods:
        // Ignite()
        // Extinguish()

// IFreezable
    // Interface for objects that can be frozen by the ice elemental effect
    // Methods:
        // Freeze()
        // Unfreeze()

// IShockable
    // Interface for objects that can be electrified by the electric elemental effect
    // Methods:
        // Electrify()
        // Deelectrify()

// IKnockbackable
    // Interface for objects that can be knocked back by the wind elemental effect
    // Methods:
        // Knockback(Vector2 direction, float force)