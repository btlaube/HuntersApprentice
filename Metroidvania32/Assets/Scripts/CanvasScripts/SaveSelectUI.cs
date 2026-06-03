using UnityEngine;

public class SaveSelectUI : CanvasController
{
    public void CloseSaveSelect()
    {
        UIStateManager.Instance.Open(UIState.MainMenu);
    }
}
