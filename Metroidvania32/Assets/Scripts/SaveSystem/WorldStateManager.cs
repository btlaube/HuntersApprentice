using UnityEngine;
using System.Collections.Generic;

public class WorldStateManager : MonoBehaviour
{
    public WorldRuntimeState CurrentState;
    private WorldRuntimeState checkpointState;

    public void InitializeNewRun(BaseGameData baseData)
    {
        CurrentState = new WorldRuntimeState
        {
            activatedFlags = new HashSet<string>()
        };
        Debug.Log("New run initialized");
    }

    public WorldSaveData GetSaveData()
    {
        return new WorldSaveData
        {
            activatedFlags = new List<string>(CurrentState.activatedFlags)
        };
    }

    public void ApplySaveData(WorldSaveData saveData)
    {
        CurrentState.activatedFlags = new HashSet<string>(saveData.activatedFlags);
    }

    public void SaveCheckpointState()
    {
        checkpointState = CloneState(CurrentState);
    }

    private WorldRuntimeState CloneState(WorldRuntimeState state)
    {
        return new WorldRuntimeState
        {
            activatedFlags = state.activatedFlags
        };
    }

    public void RestoreCheckpointState()
    {
        CurrentState = CloneState(checkpointState);
    }

}
