using UnityEngine;
using UnityEngine.UI;

public class EquippedElementSlot : MonoBehaviour
{
    [SerializeField] private Button button;

    public Button Button => button;
    [SerializeField] private InventoryUI inventoryUI;

    public string upgradeID;

    [SerializeField] private Image emptySlotIcon;
    [SerializeField] private Image equippedElementIcon;
    [SerializeField] private Sprite fireIcon;
    [SerializeField] private Sprite iceIcon;
    [SerializeField] private Sprite windIcon;
    [SerializeField] private Sprite electricityIcon;

    public void Refresh()
    {
        bool unlocked =
            InventoryDataManager.Instance.currentInventoryData.unlockedUpgrades.Contains(upgradeID);

        emptySlotIcon.enabled = unlocked;

        if (!unlocked)
            return;

        if (InventoryDataManager.Instance
            .TryGetEquippedElement(upgradeID, out string elementID))
        {
            emptySlotIcon.enabled = false;
            equippedElementIcon.enabled = true;

            // Set icon from element database
            // equippedElementIcon.sprite =
            //     ElementDatabase.Instance.GetElement(elementID).icon;
            SetIcon(elementID);
        }
        else
        {
            emptySlotIcon.enabled = true;
            equippedElementIcon.enabled = false;
        }
    }

    public void OnClick()
    {
        inventoryUI.SelectEquippedSlot(upgradeID);
    }

    private void SetIcon(string elementID)
    {
        switch(elementID)
        {
            case "fire":
                equippedElementIcon.sprite = fireIcon;
                break;
            case "ice":
                equippedElementIcon.sprite = iceIcon;
                break;
            case "wind":
                equippedElementIcon.sprite = windIcon;
                break;
            case "electricity":
                equippedElementIcon.sprite = electricityIcon;
                break;
            default:
                break;
        }

    }

}