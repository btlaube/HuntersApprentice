using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneContextManager : MonoBehaviour
{
    public static SceneContextManager Instance;

    public SceneContext CurrentContext { get; private set; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentContext = FindFirstObjectByType<SceneContext>();

        if (CurrentContext == null) return;

        ApplySceneContext(CurrentContext);
    }

    private void ApplySceneContext(SceneContext sceneContext)
    {
        switch (CurrentContext.sceneType)
        {
            case SceneType.MainMenu:
                // Handle Main Menu UI
                Debug.Log("Main Menu Loaded");
                GameStateManager.Instance.ChangeState(GameState.MainMenu);
                // UIStateManager.Instance.Open(UIState.MainMenu);
                break;
            case SceneType.Gameplay:
                // Handle Gameplay UI
                GameStateManager.Instance.ChangeState(GameState.Gameplay);
                break;
            case SceneType.Cutscene:
                // Handle Cutscene UI
                GameStateManager.Instance.ChangeState(GameState.Cutscene);
                break;
            default:
                break;
        }
    }
}
