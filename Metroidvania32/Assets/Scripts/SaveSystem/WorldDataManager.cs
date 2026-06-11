using UnityEngine;
using System.Collections.Generic;

public class WorldDataManager : MonoBehaviour
{

    public WorldSaveData currentWorldData;

    public static WorldDataManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // public void InitializeNewRun(BaseWorldData baseData)
    // {
    //     currentWorldData = new WorldSaveData
    //     {
    //         defeatedBosses = baseData.defeatedBosses,
    //         collectedItems = baseData.collectedItems,
    //         npcStates = baseData.npcStates,
    //         destroyedBreakables = baseData.destroyedBreakables,
    //         activatedLevers = baseData.activatedLevers,
    //         triggeredCutscenes = baseData.triggeredCutscenes
    //     };
    // }

    public WorldSaveData GetData()
    {
        return currentWorldData;
    }

    public void ApplyData(WorldSaveData saveData)
    {
        currentWorldData = saveData;
    }

    public void OnBossDefeated(string bossID)
    {
        currentWorldData.defeatedBosses.Add(bossID);
        GameManager.Instance.SaveGame();
    }


    public void OnDestroyedPermanentBreakable(string breakableID)
    {
        currentWorldData.destroyedBreakables.Add(breakableID);
        GameManager.Instance.SaveGame();
    }

    public void OnItemCollected(string itemID)
    {
        currentWorldData.collectedItems.Add(itemID);
        GameManager.Instance.SaveGame();
    }

}
