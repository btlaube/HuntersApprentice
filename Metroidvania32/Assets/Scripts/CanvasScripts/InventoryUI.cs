using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public enum InventoryUIState
{
    Browsing,
    SelectingElement,
    MovingEquippedElement
}


public class InventoryUI : CanvasController
{

    [SerializeField] private List<UpgradeSlot> upgradeSlots;
    [SerializeField] private List<ElementSlot> elementSlots;
    [SerializeField] private List<EquippedElementSlot> equippedElementSlots;
    [SerializeField] private Button cancelButton;

    // private string selectedElementID;
    private InventoryUIState currentState = InventoryUIState.Browsing;

    // Used when selecting an element to equip.
    private string selectedElementID;

    // Used when moving an already equipped element.
    private string selectedUpgradeID;

    public override void Show()
    {
        base.Show();
        RefreshUI();
        SetBrowsingNavigation();
    }

    public void RefreshUI()
    {
        // Update upgrade slots
        foreach (UpgradeSlot upgradeSlot in upgradeSlots)
        {
            upgradeSlot.Refresh();
        }
        // Update unlockedElement slots
        foreach (ElementSlot elementSlot in elementSlots)
        {
            elementSlot.Refresh();
        }
        // Update equipped element slots
        foreach (EquippedElementSlot equippedElementSlot in equippedElementSlots)
        {
            equippedElementSlot.Refresh();
        }
    }

    #region Element Selection
    public void SelectElement(string elementID)
    {
        if (currentState != InventoryUIState.Browsing)
            return;

        selectedElementID = elementID;
        selectedUpgradeID = null;

        currentState = InventoryUIState.SelectingElement;

        FocusFirstAvailableEquippedSlot();
        RestrictNavigationToEquippedSlotsAndCancelButton();
    }
    #endregion

    #region Equipped Slot Selection
    public void SelectEquippedSlot(string upgradeID)
    {
        switch (currentState)
        {
            case InventoryUIState.Browsing:
                HandleBrowsingEquippedSlotSelection(upgradeID);
                break;

            case InventoryUIState.SelectingElement:
                HandleElementEquip(upgradeID);
                break;

            case InventoryUIState.MovingEquippedElement:
                HandleMoveEquippedElement(upgradeID);
                break;
        }
    }
    
        private void HandleBrowsingEquippedSlotSelection(string upgradeID)
    {
        if (!HasEquippedElement(upgradeID))
            return;

        selectedUpgradeID = upgradeID;
        selectedElementID = null;

        currentState = InventoryUIState.MovingEquippedElement;

        RestrictNavigationToEquippedSlotsAndCancelButton();
    }
    #endregion

    #region Equipping New Element
    private void HandleElementEquip(string targetUpgradeID)
    {
        string previousElementID = GetEquippedElement(targetUpgradeID);

        InventoryDataManager.Instance.EquipElement(targetUpgradeID, selectedElementID);

        GameManager.Instance.SaveGame();

        selectedElementID = null;

        currentState = InventoryUIState.Browsing;

        RefreshUI();

        FocusFirstElementSlot();
        // RestrictNavigationToElementSlots();
        SetBrowsingNavigation();
    }
    #endregion

    #region Moving Equipped Elements
    private void HandleMoveEquippedElement(string targetUpgradeID)
    {
        if (string.IsNullOrEmpty(selectedUpgradeID))
            return;

        string sourceElement =
            GetEquippedElement(selectedUpgradeID);

        string targetElement =
            GetEquippedElement(targetUpgradeID);

        // Same slot selected.
        if (selectedUpgradeID == targetUpgradeID)
        {
            CancelSelection();
            return;
        }

        // Empty target slot.
        if (string.IsNullOrEmpty(targetElement))
        {
            InventoryDataManager.Instance.EquipElement(
                targetUpgradeID,
                sourceElement
            );

            InventoryDataManager.Instance.UnequipElement(
                selectedUpgradeID
            );
        }
        else
        {
            // Swap elements.
            InventoryDataManager.Instance.EquipElement(
                targetUpgradeID,
                sourceElement
            );

            InventoryDataManager.Instance.EquipElement(
                selectedUpgradeID,
                targetElement
            );
        }

        GameManager.Instance.SaveGame();

        selectedUpgradeID = null;

        currentState = InventoryUIState.Browsing;

        RefreshUI();

        FocusFirstElementSlot();
        // RestrictNavigationToElementSlots();
        SetBrowsingNavigation();
    }
    #endregion

    #region Cancel Actions
    public void OnElementSectionSelected()
    {
        switch (currentState)
        {
            case InventoryUIState.SelectingElement:
                CancelSelection();
                break;

            case InventoryUIState.MovingEquippedElement:
                UnequipSelectedElement();
                break;
        }
    }

    private void CancelSelection()
    {
        selectedElementID = null;
        selectedUpgradeID = null;

        currentState = InventoryUIState.Browsing;

        FocusFirstElementSlot();
        // RestrictNavigationToElementSlots();
        SetBrowsingNavigation();
    }

    private void UnequipSelectedElement()
    {
        if (string.IsNullOrEmpty(selectedUpgradeID))
            return;

        // InventoryDataManager.Instance.currentInventoryData.equippedElements[selectedUpgradeID] = null;
        // InventoryDataManager.Instance.currentInventoryData.equippedElements.Remove(selectedUpgradeID);
        InventoryDataManager.Instance.UnequipElement(
                selectedUpgradeID
            );
        GameManager.Instance.SaveGame();

        selectedUpgradeID = null;

        currentState = InventoryUIState.Browsing;

        RefreshUI();

        FocusFirstElementSlot();
        // RestrictNavigationToElementSlots();
        SetBrowsingNavigation();
    }
    #endregion

    #region Helpers
    private bool HasEquippedElement(string upgradeID)
    {
        // return !string.IsNullOrEmpty(GetEquippedElement(upgradeID));
        return InventoryDataManager.Instance.GetEquippedElement(upgradeID) != null;
    }

    private string GetEquippedElement(string upgradeID)
    {
        if (InventoryDataManager.Instance
            .TryGetEquippedElement(upgradeID, out string elementID))
        {
            return elementID;
        }

        return null;
    }
    #endregion

    private void SetupHorizontalNavigation(List<Selectable> selectables)
    {
        if (selectables.Count == 0)
            return;

        for (int i = 0; i < selectables.Count; i++)
        {
            Navigation nav = selectables[i].navigation;
            nav.mode = Navigation.Mode.Explicit;

            nav.selectOnLeft =
                selectables[(i - 1 + selectables.Count) % selectables.Count];

            nav.selectOnRight =
                selectables[(i + 1) % selectables.Count];

            selectables[i].navigation = nav;
        }
    }

    private void SetBrowsingNavigation()
    {
        List<Selectable> equippedButtons = GetVisibleEquippedButtons();
        List<Selectable> elementButtons = GetVisibleElementButtons();

        SetupHorizontalNavigation(equippedButtons);
        SetupHorizontalNavigation(elementButtons);

        Selectable firstEquipped = GetFirstEquippedSlot();
        Selectable firstElement = GetFirstElementSlot();

        foreach (Selectable element in elementButtons)
        {
            Navigation nav = element.navigation;

            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnUp = firstEquipped;
            nav.selectOnDown = null;

            element.navigation = nav;
        }

        foreach (Selectable equipped in equippedButtons)
        {
            Navigation nav = equipped.navigation;

            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnUp = null;
            nav.selectOnDown = firstElement;

            equipped.navigation = nav;
        }
    }

    private void RestrictNavigationToElementSlots()
    {
        List<Selectable> elementButtons = GetVisibleElementButtons();

        SetupHorizontalNavigation(elementButtons);

        foreach (Selectable element in elementButtons)
        {
            Navigation nav = element.navigation;

            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnUp = null;
            nav.selectOnDown = null;

            element.navigation = nav;
        }

        Selectable firstElement = GetFirstElementSlot();

        if (firstElement != null)
        {
            EventSystem.current.SetSelectedGameObject(
                firstElement.gameObject);
        }
    }

    private void RestrictNavigationToEquippedSlotsAndCancelButton()
    {
        List<Selectable> equippedButtons = GetVisibleEquippedButtons();

        SetupHorizontalNavigation(equippedButtons);

        foreach (Selectable equipped in equippedButtons)
        {
            Navigation nav = equipped.navigation;

            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnUp = null;
            nav.selectOnDown = cancelButton;

            equipped.navigation = nav;
        }

        Navigation cancelNav = cancelButton.navigation;

        cancelNav.mode = Navigation.Mode.Explicit;
        cancelNav.selectOnUp = GetFirstEquippedSlot();
        cancelNav.selectOnDown = null;
        cancelNav.selectOnLeft = null;
        cancelNav.selectOnRight = null;

        cancelButton.navigation = cancelNav;

        Selectable firstEquipped = GetFirstEquippedSlot();

        if (firstEquipped != null)
        {
            EventSystem.current.SetSelectedGameObject(
                firstEquipped.gameObject);
        }
    }

    private List<Selectable> GetVisibleElementButtons()
    {
        List<Selectable> buttons = new();

        foreach (ElementSlot slot in elementSlots)
        {
            if (!InventoryDataManager.Instance.currentInventoryData.unlockedElements.Contains(slot.elementID) || slot.Button.interactable == false)
            {
                continue;
            }

            buttons.Add(slot.Button);
        }

        return buttons;
    }

    private List<Selectable> GetVisibleEquippedButtons()
    {
        List<Selectable> buttons = new();

        foreach (EquippedElementSlot slot in equippedElementSlots)
        {
            if (!InventoryDataManager.Instance.currentInventoryData.unlockedUpgrades
                .Contains(slot.upgradeID))
            {
                continue;
            }

            buttons.Add(slot.Button);
        }

        return buttons;
    }

    private Selectable GetFirstElementSlot()
    {
        List<Selectable> elements = GetVisibleElementButtons();

        return elements.Count > 0 ? elements[0] : null;
    }

    private Selectable GetFirstEquippedSlot()
    {
        List<Selectable> equipped = GetVisibleEquippedButtons();

        return equipped.Count > 0 ? equipped[0] : null;
    }

    private void SetSelectedObject(GameObject selectedObject)
    {
        if (EventSystem.current == null)
            return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedObject);
    }

    private void FocusFirstElementSlot()
    {
        Selectable firstElement = GetFirstElementSlot();

        if (firstElement == null)
            return;

        SetSelectedObject(firstElement.gameObject);
    }

    private void FocusFirstAvailableEquippedSlot()
    {
        Selectable firstEquipped = GetFirstEquippedSlot();

        if (firstEquipped == null)
            return;

        SetSelectedObject(firstEquipped.gameObject);
    }
}
