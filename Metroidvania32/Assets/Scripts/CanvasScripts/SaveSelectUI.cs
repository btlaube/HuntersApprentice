using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SaveSelectUI : CanvasController
{

    [SerializeField] private MainMenuUIManager mainMenuUIManager;
    // [SerializeField] private GameObject firstSelected;

    public Transform saveSlotParent;
    public GameObject saveSlotButtonPrefab;

    private Dictionary<int, SaveSlotButton> saveSlotButtons = new Dictionary<int, SaveSlotButton>();

    public override void Show()
    {
        base.Show();
        // InitializeSaveSelect();
    }

    void Start()
    {
        InitializeSaveSelect();
    }

    public void InitializeSaveSelect()
    {
        PopulateSaveList();
    }

    // public void SetFirstSelected()
    // {
    //     StartCoroutine(SetFirstSelectedWithDelay(firstSelected));
    // }

    // public IEnumerator SetFirstSelectedWithDelay(GameObject obj)
    // {
    //     yield return new WaitForEndOfFrame();
    //     EventSystem.current.SetSelectedGameObject(obj);
    // }

    public void CloseSaveSelect()
    {
        // UIStateManager.Instance.Open(UIState.MainMenu);
        mainMenuUIManager.CloseSaveSelect();
    }

    public void OnNewGameSelected()
    {
        GameManager.Instance.OnNewGame();
    }

    public void OnSaveSlotSelected(int slot)
    {
        
    }

    private void PopulateSaveList()
    {
        List<Metadata> saves =
            SaveManager.Instance.GetAllMetadata();

        foreach (Metadata metadata in saves)
        {
            GameObject saveSlotObject =
                Instantiate(saveSlotButtonPrefab, saveSlotParent.position, Quaternion.identity, saveSlotParent);

            SaveSlotButton saveSlotButton = saveSlotObject.GetComponent<SaveSlotButton>();
            
            // button.transform.SetParent(saveSlotParent, false);
            saveSlotButton.Initialize(metadata);

            // button.GetComponent<SaveSlotButton>().OnSelected += HandleSaveSelected;
            saveSlotButton.OnLoaded += HandleSaveLoaded;
            saveSlotButton.OnDeleted += HandleSaveDeleted;
            saveSlotButton.OnCanceled += HandleSaveCanceled;

            saveSlotButtons[metadata.slotNumber] = saveSlotButton;
        }
    }

    // private void HandleSaveSelected(int slot)
    // {
    //     // Open delete or load panel
    //     // 
    // }

    private void HandleSaveLoaded(int slot)
    {
        GameManager.Instance.OnLoadGame(slot);
    }

    private void HandleSaveDeleted(int slot)
    {
        SaveManager.Instance.DeleteSave(slot);
        DeleteSaveSlotButton(slot);
        this.SetFirstSelected();
    }

    private void HandleSaveCanceled(int slot)
    {
        this.SetFirstSelected();
    }

    private void DeleteSaveSlotButton(int slot)
    {
        if (saveSlotButtons.TryGetValue(slot, out SaveSlotButton button))
        {
            saveSlotButtons.Remove(slot);
            Destroy(button.gameObject);
        }
    }

}
