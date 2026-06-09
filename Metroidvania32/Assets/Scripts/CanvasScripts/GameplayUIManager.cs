using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{

    [SerializeField] private CanvasController hudUI;
    [SerializeField] private CanvasController inventoryUI;
    [SerializeField] private CanvasController mapUI;
    [SerializeField] private CanvasController pauseUI;
    [SerializeField] private CanvasController dialogueUI;
    // [SerializeField] private JournalUI journalUI;
    [SerializeField] private CanvasController optionsUI;

    private bool pauseOpen;
    private bool mapOpen;
    private bool inventoryOpen;

    private void OnEnable()
    {
        MasterInputHandler.Instance.uiInput.OnPause += TogglePause;
        MasterInputHandler.Instance.uiInput.OnInventory += ToggleInventory;
        MasterInputHandler.Instance.uiInput.OnMap += ToggleMap;
    }

    private void OnDisable()
    {
        MasterInputHandler.Instance.uiInput.OnPause -= TogglePause;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpenHUD();
        CloseInventory();
        CloseMap();
        ClosePause();
        CloseDialogue();
        CloseOptions();
    }

    public void TogglePause()
    {
        if (pauseOpen)
        {
            ClosePause();
            pauseOpen = false;
            // PauseManager.Paused = false;
        }
        else if (inventoryOpen)
        {
            CloseInventory();
            inventoryOpen = false;
            OpenPause();
            pauseOpen = true;
        }
        else if (mapOpen)
        {
            CloseMap();
            mapOpen = false;
            OpenPause();
            pauseOpen = true;
        }
        else
        {
            OpenPause();
            pauseOpen = true;
            // PauseManager.Paused = true;
        }
    }

    public void ToggleMap()
    {
        if (mapOpen)
        {
            CloseMap();
            mapOpen = false;
            // PauseManager.Paused = false;
        }
        else if (pauseOpen)
        {
            ClosePause();
            pauseOpen = false;
            OpenMap();
            mapOpen = true;
        }
        else if (inventoryOpen)
        {
            CloseInventory();
            inventoryOpen = false;
            OpenMap();
            mapOpen = true;
        }
        else
        {
            OpenMap();
            mapOpen = true;
            // PauseManager.Paused = true;
        }
    }

    public void ToggleInventory()
    {
        if (inventoryOpen)
        {
            CloseInventory();
            inventoryOpen = false;
            // PauseManager.Paused = false;
        }
        else if (pauseOpen)
        {
            ClosePause();
            pauseOpen = false;
            OpenInventory();
            inventoryOpen = true;
        }
        else if (mapOpen)
        {
            CloseMap();
            mapOpen = false;
            OpenInventory();
            inventoryOpen = true;
        }
        else
        {
            OpenInventory();
            inventoryOpen = true;
            // PauseManager.Paused = true;
        }
    }

    public void OpenHUD()
    {
        hudUI.Show();
    }

    public void OpenPause()
    {
        pauseUI.Show();
    }

    public void OpenInventory()
    {
        inventoryUI.Show();
    }

    public void OpenMap()
    {
        mapUI.Show();
    }

    public void OpenDialogue()
    {
        dialogueUI.Show();
    }

    public void OpenOptions()
    {
        optionsUI.Show();
    }

    public void CloseHUD()
    {
        hudUI.Hide();
    }

    public void ClosePause()
    {
        pauseUI.Hide();
    }

    public void CloseInventory()
    {
        inventoryUI.Hide();
    }

    public void CloseMap()
    {
        mapUI.Hide();
    }

    public void CloseOptions()
    {
        optionsUI.Hide();
    }

    public void CloseDialogue()
    {
        dialogueUI.Hide();
    }

}
