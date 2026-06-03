using UnityEngine;

public class RunManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerBaseData baseData;

    public PlayerRuntimeState CurrentPlayerState;
    private PlayerRuntimeState checkpointState;

    public static RunManager Instance;

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

    private void Start()
    {
        // InitializeNewRun();
    }

    public void InitializeNewRun()
    {
        CurrentPlayerState = new PlayerRuntimeState
        {
            maxHealth = baseData.startingMaxHealth,
            currentHealth = baseData.startingMaxHealth,
            moveSpeed = baseData.startingMoveSpeed,
            // position = Vector3.zero,
            spawnPointData = baseData.startingSpawnPoint
        };
        Debug.Log("New run initialized");

        SaveCheckpoint();
    }

    public void SaveCheckpoint()
    {
        checkpointState = CloneState(CurrentPlayerState);
    }

    public void Respawn()
    {
        CurrentPlayerState = CloneState(checkpointState);

        // transform.position = CurrentPlayerState.position;
        // transform.position = CurrentPlayerState.spawnPointData.spawnPoint;
    }

    private PlayerRuntimeState CloneState(PlayerRuntimeState state)
    {
        return new PlayerRuntimeState
        {
            maxHealth = state.maxHealth,
            currentHealth = state.currentHealth,
            moveSpeed = state.moveSpeed,
            // position = state.position
            spawnPointData = state.spawnPointData
        };
    }
}
