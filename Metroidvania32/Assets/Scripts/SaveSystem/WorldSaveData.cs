using System.Collections.Generic;
[System.Serializable]
public class WorldSaveData
{
    public List<string> activatedFlags = new();

    // public void GetSaveData(WorldRuntimeState state)
    // {
    //     activatedFlags = state.activatedFlags;
    // }

    // public void ApplySaveData(WorldRuntimeState state)
    // {
    //     state.activatedFlags = activatedFlags;
    // }

}