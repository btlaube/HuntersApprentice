using UnityEngine;

public class UpgradePickup : CollectableWorldItem
{
    public override void Interact()
    {
        InventoryDataManager.Instance.OnUpgradeCollected(this.flagID);
        WorldDataManager.Instance.OnItemCollected(this.flagID);
        Destroy(gameObject);
    }
}
