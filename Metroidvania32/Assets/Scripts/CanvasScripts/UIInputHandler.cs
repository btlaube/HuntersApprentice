using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class UIInputHandler : MonoBehaviour
{
    public Vector2 menuNavigationInput { get; private set; }

    public event Action OnSubmit;
    public event Action OnCancel;
    public event Action OnPause;

    private PlayerControls controls;

    // public UIInputHandler(PlayerControls controls)
    // {
    //     this.controls = controls;

    //     controls.UI.Navigate.performed += ctx =>
    //     {
    //         menuNavigationInput = ctx.ReadValue<Vector2>();
    //     };

    //     controls.UI.Navigate.canceled += ctx =>
    //     {
    //         menuNavigationInput = Vector2.zero;
    //     };

    //     controls.UI.Submit.performed += ctx =>
    //     {
    //         OnSubmit?.Invoke();
    //     };
    // }

    // public void AssignControls(PlayerControls controls)
    // {
    //     this.controls = controls;

    //     controls.UI.Navigate.performed += ctx =>
    //     {
    //         menuNavigationInput = ctx.ReadValue<Vector2>();
    //     };

    //     controls.UI.Navigate.canceled += ctx =>
    //     {
    //         menuNavigationInput = Vector2.zero;
    //     };

    //     controls.UI.Submit.performed += ctx =>
    //     {
    //         OnSubmit?.Invoke();
    //     };

    //     controls.UI.Cancel.performed += ctx =>
    //     {
    //         OnCancel?.Invoke();
    //     };

    //     // controls.UI.Pause.performed += ctx =>
    //     // {
    //     //     // OnPause?.Invoke();
    //     //     TogglePause();
    //     // };
    //     controls.UI.Pause.performed += TogglePause;
    // }
    public void AssignControls(PlayerControls newControls)
    {
        // Unsubscribe from old controls
        if (controls != null)
        {
            controls.UI.Pause.performed -= TogglePause;
            controls.UI.Inventory.performed -= ToggleInventory;
            controls.UI.Map.performed -= ToggleMap;

            controls.Disable();
        }

        // Assign new controls
        controls = newControls;

        if (controls != null)
        {
            controls.Enable();

            controls.UI.Pause.performed += TogglePause;
            controls.UI.Inventory.performed += ToggleInventory;
            controls.UI.Map.performed += ToggleMap;
        }
    }

    public void TogglePause(InputAction.CallbackContext ctx)
    {
        // OnPause?.Invoke();
        // GameManager.Instance.TogglePause();
        // GameManager.Instance.SwitchState(GameState.Paused);
        // Pauses game and opens settings menu. If already paused, unpauses and closes menu.
        UIManager.Instance.OnPausePressed();
    }
    public void ToggleInventory(InputAction.CallbackContext ctx)
    {
        // GameManager.Instance.SwitchState(GameState.Inventory);
        // Pauses game and opens inventory. If already open, unpauses and closes inventory.
        UIManager.Instance.OnInventoryPressed();
    }
    public void ToggleMap(InputAction.CallbackContext ctx)
    {
        // GameManager.Instance.SwitchState(GameState.Map);
        // Pauses game and opens map. If already open, unpauses and closes map.
        UIManager.Instance.OnMapPressed();
    }

}
