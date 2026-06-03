using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : CanvasController
{
    [SerializeField] private PlayerHealthUI healthUI;

    // public void ConnectPlayerHealth(PlayerHealth playerHealth)
    // {
    //     healthUI.Initialize(playerHealth);
    // }

    public override void Show()
    {
        base.Show();
        InitializeHUD();
    }

    public void InitializeHUD()
    {
        healthUI.Initialize();
    }
}