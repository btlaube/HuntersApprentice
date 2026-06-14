using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public bool PlayerPaused;
    public bool WorldPaused;

    public static PauseManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void SetPlayerPaused(bool paused)
    {
        PlayerPaused = paused;
    }

    public void SetWorldPaused(bool paused)
    {
        WorldPaused = paused;
    }

    public void SetGamePaused(bool paused)
    {
        SetPlayerPaused(paused);
        SetWorldPaused(paused);
    }

}
