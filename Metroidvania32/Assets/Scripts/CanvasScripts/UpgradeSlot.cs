using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private string upgradeID;
    [SerializeField] private Image upgradeIcon;

    public void Refresh()
    {
        bool unlocked =
            InventoryDataManager.Instance.currentInventoryData.unlockedUpgrades.Contains(upgradeID);

        upgradeIcon.enabled = unlocked;
    }

}