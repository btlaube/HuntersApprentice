using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class SaveManager : MonoBehaviour
{

    [SerializeField] private BaseGameData baseData;

    public static SaveManager Instance { get; private set; }

    public int CurrentSlot { get; private set; } = -1;

    private string SaveDirectory =>
        Path.Combine(Application.persistentDataPath, "Saves");

    private void Awake()
    {
        Debug.Log("SaveManager Awake");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Directory.CreateDirectory(SaveDirectory);
    }

    #region New Game

    public int CreateNewSave()
    {
        int slot = GetNextAvailableSlot();

        GameData gameData = CreateDefaultGameData(baseData);

        WriteGameData(slot, gameData);
        WriteMetadata(slot, BuildMetadata(slot, gameData));

        CurrentSlot = slot;

        return slot;
    }

    #endregion

    #region Save

    public void SaveGame()
    {
        if (CurrentSlot < 0)
        {
            Debug.LogError("No active save slot.");
            return;
        }

        GameData gameData = BuildGameData();

        WriteGameData(CurrentSlot, gameData);
        WriteMetadata(CurrentSlot, BuildMetadata(CurrentSlot, gameData));

        Debug.Log($"Saved slot {CurrentSlot}");
    }

    #endregion

    #region Load

    public GameData LoadGame(int slot)
    {
        string savePath = GetSavePath(slot);

        if (!File.Exists(savePath))
        {
            Debug.LogWarning($"No save file found for slot {slot}");
            return null;
        }

        string json = File.ReadAllText(savePath);

        GameData gameData =
            JsonUtility.FromJson<GameData>(json);

        CurrentSlot = slot;

        GameDataManager.Instance.SetGameData(gameData);

        Debug.Log($"Loaded slot {slot}");

        return gameData;
    }

    #endregion

    #region Delete

    public void DeleteSave(int slot)
    {
        string savePath = GetSavePath(slot);
        string metaPath = GetMetadataPath(slot);

        if (File.Exists(savePath))
            File.Delete(savePath);

        if (File.Exists(metaPath))
            File.Delete(metaPath);

        if (CurrentSlot == slot)
            CurrentSlot = -1;
    }

    #endregion

    #region Paths

    private string GetSavePath(int slot)
    {
        return Path.Combine(
            SaveDirectory,
            $"save_slot_{slot}.json");
    }

    private string GetMetadataPath(int slot)
    {
        return Path.Combine(
            SaveDirectory,
            $"save_slot_{slot}_meta.json");
    }

    #endregion

    #region File IO

    private void WriteGameData(int slot, GameData data)
    {
        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(
            GetSavePath(slot),
            json);
    }

    private void WriteMetadata(int slot, Metadata metadata)
    {
        string json = JsonUtility.ToJson(metadata, true);

        File.WriteAllText(
            GetMetadataPath(slot),
            json);
    }

    public Metadata LoadMetadata(int slot)
    {
        string path = GetMetadataPath(slot);

        if (!File.Exists(path))
            return null;

        return JsonUtility.FromJson<Metadata>(
            File.ReadAllText(path));
    }

    #endregion

    #region Slot Discovery

    public List<int> GetExistingSlots()
    {
        if (!Directory.Exists(SaveDirectory))
            return new List<int>();

        return Directory
            .GetFiles(SaveDirectory, "save_slot_*.json")
            .Where(path => !path.Contains("_meta"))
            .Select(ParseSlotNumber)
            .OrderBy(slot => slot)
            .ToList();
    }

    public List<Metadata> GetAllMetadata()
    {
        List<Metadata> metadataList = new();

        foreach (int slot in GetExistingSlots())
        {
            Metadata metadata = LoadMetadata(slot);

            if (metadata != null)
            {
                metadataList.Add(metadata);
            }
        }

        return metadataList;
    }

    private int GetNextAvailableSlot()
    {
        List<int> slots = GetExistingSlots();

        int slot = 0;

        while (slots.Contains(slot))
            slot++;

        return slot;
    }

    private int ParseSlotNumber(string path)
    {
        string fileName =
            Path.GetFileNameWithoutExtension(path);

        string number =
            fileName.Replace("save_slot_", "");

        return int.Parse(number);
    }

    #endregion

    #region Data Builders

    private GameData BuildGameData()
    {
        return GameDataManager.Instance.GetGameData();
    }

    private Metadata BuildMetadata(
        int slot,
        GameData gameData)
    {
        return new Metadata
        {
            slotNumber = slot,
            slotName = $"Save Slot {slot}",
            lastSaveDate = DateTime.Now.ToString("O")
        };
    }

    private GameData CreateDefaultGameData(BaseGameData baseData)
    {
        // return new GameData();
        return new GameData
        {
            player = new PlayerSaveData
            {
                maxHealth = baseData.basePlayerData.startingMaxHealth,
                currentHealth = baseData.basePlayerData.startingMaxHealth,
                moveSpeed = baseData.basePlayerData.startingMoveSpeed,
                spawnPointData = baseData.basePlayerData.startingSpawnPoint
            },
            world = new WorldSaveData
            {
                defeatedBosses = baseData.baseWorldData.defeatedBosses,
                collectedItems = baseData.baseWorldData.collectedItems,
                npcStates = baseData.baseWorldData.npcStates,
                destroyedBreakables = baseData.baseWorldData.destroyedBreakables,
                activatedLevers = baseData.baseWorldData.activatedLevers,
                triggeredCutscenes = baseData.baseWorldData.triggeredCutscenes
            }
        };
    }

    #endregion
}