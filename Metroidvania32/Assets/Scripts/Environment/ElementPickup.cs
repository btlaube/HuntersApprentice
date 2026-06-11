using UnityEngine;

public class ElementPickup : CollectableWorldItem
{
    public override void Interact()
    {
        InventoryDataManager.Instance.OnElementCollected(this.flagID);
        WorldDataManager.Instance.OnItemCollected(this.flagID);
        Destroy(gameObject);
    }
}
