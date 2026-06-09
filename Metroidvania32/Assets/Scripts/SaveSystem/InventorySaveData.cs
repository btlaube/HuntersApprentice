using System.Collections.Generic;

public enum ElementType
{
    None,
    Fire,
    Ice,
    Lightning,
    Poison
}

public enum EquipmentSlot
{
    Melee,
    Ranged,
    Dash
}

[System.Serializable]
public class InventorySaveData
{
    public List<string> unlockedUpgrades;
    public List<ElementType> unlockedElements;
    public Dictionary<EquipmentSlot, ElementType> equippedElements;
}