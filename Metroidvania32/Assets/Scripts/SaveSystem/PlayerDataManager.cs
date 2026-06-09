using UnityEngine;
using System;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerSaveData Data;
    public event Action<float, float> OnHealthChanged;

    public static PlayerDataManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // public void InitializeNewRun(BasePlayerData baseData)
    // {
    //     Data = new PlayerSaveData
    //     {
    //         maxHealth = baseData.startingMaxHealth,
    //         currentHealth = baseData.startingMaxHealth,
    //         moveSpeed = baseData.startingMoveSpeed,
    //         spawnPointData = baseData.startingSpawnPoint
    //     };
    // }

    public PlayerSaveData GetData()
    {
        return Data;
    }

    public void ApplyData(PlayerSaveData saveData)
    {
        Data = saveData;
    }

    public void SetHealth(float health)
    {
        Data.currentHealth = health;
        OnHealthChanged?.Invoke(Data.currentHealth, Data.maxHealth);
    }

}