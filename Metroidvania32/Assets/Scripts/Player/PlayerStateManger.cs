using UnityEngine;
using System;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerRuntimeState CurrentState;
    private PlayerRuntimeState checkpointState;

    public event Action<float, float> OnHealthChanged;

    // private void Start()
    // {
    //     CurrentState = RunManager.Instance.CurrentPlayerState;
    // }

    public void InitializeNewRun(BaseGameData baseData)
    {
        CurrentState = new PlayerRuntimeState
        {
            maxHealth = baseData.playerBaseData.startingMaxHealth,
            currentHealth = baseData.playerBaseData.startingMaxHealth,
            moveSpeed = baseData.playerBaseData.startingMoveSpeed,
            spawnPointData = baseData.playerBaseData.startingSpawnPoint
        };
        // Debug.Log("New run initialized");
    }

    public PlayerSaveData GetSaveData()
    {
        return new PlayerSaveData
        {
            currentHealth = CurrentState.currentHealth,
            maxHealth = CurrentState.maxHealth,
            moveSpeed = CurrentState.moveSpeed,
            spawnPointData = CurrentState.spawnPointData
        };
    }

    public void ApplySaveData(PlayerSaveData saveData)
    {
        CurrentState.maxHealth = saveData.maxHealth;
        CurrentState.currentHealth = saveData.currentHealth;
        CurrentState.moveSpeed = saveData.moveSpeed;
        CurrentState.spawnPointData = saveData.spawnPointData;
    }

    public void SaveCheckpointState()
    {
        checkpointState = CloneState(CurrentState);
    }

    public void RestoreCheckpointState()
    {
        CurrentState = CloneState(checkpointState);

        // Trigger health update to refresh UI after restoring checkpoint
        // OnHealthChanged?.Invoke(CurrentState.currentHealth, CurrentState.maxHealth);
        // TODO: Add UI update event to GameManager
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

    public void UpdateRuntimeState(PlayerRuntimeState newState)
    {
        CurrentState = newState;
    }

    public void SetHealth(float health)
    {
        CurrentState.currentHealth = health;
        OnHealthChanged?.Invoke(CurrentState.currentHealth, CurrentState.maxHealth);
    }

}