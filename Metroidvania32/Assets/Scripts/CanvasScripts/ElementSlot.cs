using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ElementSlot : MonoBehaviour
{
    [SerializeField] private Button button;

    public Button Button => button;
    [SerializeField] private InventoryUI inventoryUI;
    public string elementID;
    [SerializeField] private Image elementIcon;

    public void Refresh()
    {
        bool unlocked =
            InventoryDataManager.Instance.currentInventoryData.unlockedElements.Contains(elementID);

        // elementIcon.enabled = unlocked;


        // emptySlotIcon.enabled = unlocked;

        if (!unlocked)
            return;

        if (InventoryDataManager.Instance.currentInventoryData.equippedElements
            .Any(x => x.elementID == elementID))
        {
            elementIcon.enabled = false;
            button.interactable = false;
        }
        else
        {
            elementIcon.enabled = true;
            button.interactable = true;
        }
    }

    public void OnClick()
    {
        inventoryUI.SelectElement(elementID);
    }

}