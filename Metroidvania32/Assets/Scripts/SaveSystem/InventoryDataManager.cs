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

    public void OnUpgradeCollected(string upgradeID)
    {
        currentInventoryData.unlockedUpgrades.Add(upgradeID);
        // currentInventoryData.equippedElements[upgradeID] = null;
    }

    public void OnElementCollected(string elementID)
    {
        currentInventoryData.unlockedElements.Add(elementID);
    }

    public void EquipElement(string upgradeID, string elementID)
    {
        var entry = currentInventoryData.equippedElements.Find(x => x.upgradeID == upgradeID);

        if (entry != null)
        {
            entry.elementID = elementID;
        }
        else
        {
            currentInventoryData.equippedElements.Add(new ElementSlotEntry
            {
                upgradeID = upgradeID,
                elementID = elementID
            });
        }
    }

    public void UnequipElement(string upgradeID)
    {
        var entry = currentInventoryData.equippedElements.Find(x => x.upgradeID == upgradeID);

        if (entry != null)
            currentInventoryData.equippedElements.Remove(entry);
    }

    public string GetEquippedElement(string upgradeID)
    {
        var entry = currentInventoryData.equippedElements.Find(x => x.upgradeID == upgradeID);
        return entry != null ? entry.elementID : null;
    }

    public bool TryGetEquippedElement(string upgradeID, out string elementID)
    {
        var entry = currentInventoryData.equippedElements.Find(x => x.upgradeID == upgradeID);

        if (entry != null)
        {
            elementID = entry.elementID;
            return true;
        }

        elementID = null;
        return false;
    }

}