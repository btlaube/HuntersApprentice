using System.Collections.Generic;
using System.Linq;

// public enum ElementType
// {
//     None,
//     Fire,
//     Ice,
//     Lightning,
//     Poison
// }

public enum EquipmentSlot
{
    Melee,
    Ranged,
    Dash
}

[System.Serializable]
public class ElementSlotEntry
{
    public string upgradeID;
    public string elementID;
}

[System.Serializable]
public class InventorySaveData
{
    public List<string> unlockedUpgrades = new List<string>();
    public List<string> unlockedElements = new List<string>();
    // public Dictionary<string, string> equippedElements = new Dictionary<string, string>();
    public List<ElementSlotEntry> equippedElements = new List<ElementSlotEntry>();
}