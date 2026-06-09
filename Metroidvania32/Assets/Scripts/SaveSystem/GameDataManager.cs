using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    public BaseGameData baseData;

    // public PlayerDataManager.Instance PlayerDataManager.Instance {get; private set;}
    // public WorldDataManager.Instance WorldDataManager.Instance {get; private set;}

    public static GameDataManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public GameData GetGameData()
    {
        GameData gameData = new GameData
        {
            player = PlayerDataManager.Instance.GetData(),
            world = WorldDataManager.Instance.GetData(),
            inventory = InventoryDataManager.Instance.GetData()
        };

        return gameData;
    }

    public void SetGameData(GameData gameData)
    {
        PlayerDataManager.Instance.ApplyData(gameData.player);
        WorldDataManager.Instance.ApplyData(gameData.world);
        InventoryDataManager.Instance.ApplyData(gameData.inventory);
    }

    // public void InitializeNewRun()
    // {
    //     PlayerDataManager.Instance.InitializeNewRun(baseData.playerBaseData);
    //     WorldDataManager.Instance.InitializeNewRun(baseData.worldBaseData);
    // }
}