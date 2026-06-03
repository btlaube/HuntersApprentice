using System;
using UnityEngine;

public enum GameState
{
    MainMenu,

    Gameplay,

    Menu,

    Dialogue,

    Cutscene,

    PlayerDead,

    Respawning,
    Loading
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public GameState CurrentState;// { get; private set; }

    public bool GameplayEnabled =>
        CurrentState == GameState.Gameplay;

    public event Action<GameState> OnStateEntered;
    public event Action<GameState> OnStateExited;
    public event Action<GameState, GameState> OnStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    public bool ChangeState(GameState newState)
    {
        if (newState == CurrentState)
            return false;

        if (!CanTransition(CurrentState, newState))
            return false;

        GameState previousState = CurrentState;

        ExitState(CurrentState);

        CurrentState = newState;

        EnterState(CurrentState);

        OnStateChanged?.Invoke(previousState, CurrentState);

        return true;
    }

    private bool CanTransition(GameState from, GameState to)
    {
        switch (from)
        {
            case GameState.MainMenu:
                return to == GameState.Gameplay;

            case GameState.Gameplay:
                return to == GameState.MainMenu ||
                        to == GameState.Menu ||
                        to == GameState.Dialogue ||
                        to == GameState.Cutscene ||
                        to == GameState.Loading ||
                        to == GameState.PlayerDead;

            case GameState.Menu:
                return to == GameState.MainMenu ||
                        to == GameState.Gameplay ||
                       to == GameState.Dialogue ||
                       to == GameState.Cutscene ||
                       to == GameState.Loading ||
                       to == GameState.PlayerDead;

            case GameState.Dialogue:
                return to == GameState.Gameplay ||
                       to == GameState.PlayerDead;

            case GameState.Cutscene:
                return to == GameState.Gameplay ||
                       to == GameState.Menu ||
                       to == GameState.Loading ||
                       to == GameState.PlayerDead;

            case GameState.PlayerDead:
                return to == GameState.Respawning;

            case GameState.Respawning:
                return to == GameState.Gameplay;
            case GameState.Loading:
                return to == GameState.Gameplay ||
                       to == GameState.Menu ||
                       to == GameState.Dialogue ||
                       to == GameState.Cutscene ||
                       to == GameState.PlayerDead;
        }

        return false;
    }

    private void EnterState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                UIStateManager.Instance.Open(UIState.MainMenu);
                break;
            case GameState.Gameplay:
                break;

            case GameState.Menu:
                break;
            case GameState.Dialogue:
                break;
            case GameState.Cutscene:
                break;
            case GameState.PlayerDead:
                break;
            case GameState.Respawning:
                break;
        }

        OnStateEntered?.Invoke(state);
    }

    private void ExitState(GameState state)
    {
        OnStateExited?.Invoke(state);
    }
}