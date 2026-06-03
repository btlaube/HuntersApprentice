using UnityEngine;

public class MasterInputHandler : MonoBehaviour
{

    public PlayerControls Controls { get; private set; }

    public PlayerInputHandler playerInput { get; private set; }
    public UIInputHandler uiInput { get; private set; }


    public static MasterInputHandler Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy(gameObject);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(gameObject);
        Controls = new PlayerControls();
        
        playerInput = GetComponent<PlayerInputHandler>();
        uiInput = GetComponent<UIInputHandler>();
    }

    void Start()
    {

        playerInput.AssignControls(Controls);
        uiInput.AssignControls(Controls);
        
    }

    private void OnEnable()
    {
        if (Controls == null) return;
        Controls.Enable();
    }

    private void OnDisable()
    {
        if (Controls == null) return;
        Controls.Disable();
    }
}
