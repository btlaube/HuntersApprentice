using System.Collections.Generic;
[System.Serializable]
public class WorldSaveData
{
    public List<string> defeatedBosses = new();
    public List<string> collectedItems = new();
    public List<string> npcStates = new();
    public List<string> destroyedBreakables = new();
    public List<string> activatedLevers = new();
    public List<string> triggeredCutscenes = new();

}