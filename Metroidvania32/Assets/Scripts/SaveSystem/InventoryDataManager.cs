using UnityEngine;

public class InventoryDataManager : MonoBehaviour
{
    public InventorySaveData currentInventoryData;

    public static InventoryDataManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public InventorySaveData GetData()
    {
        return currentInventoryData;
    }

    public void ApplyData(InventorySaveData saveData)
    {
        currentInventoryData = saveData;
    }
}