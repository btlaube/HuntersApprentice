using UnityEngine;

public class Upgrade : CollectableWorldItem
{
    public override void Interact()
    {
        InventoryDataManager.Instance.currentInventoryData.unlockedUpgrades.Add(this.flagID);
        WorldDataManager.Instance.currentWorldData.collectedItems.Add(flagID);
        Destroy(gameObject);
    }
}
