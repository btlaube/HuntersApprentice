using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "World/Base Data", fileName = "WorldBaseData")]
public class BaseWorldData : ScriptableObject
{
    public List<string> defeatedBosses = new();
    public List<string> collectedItems = new();
    public List<string> npcStates = new();
    public List<string> destroyedBreakables = new();
    public List<string> activatedLevers = new();
    public List<string> triggeredCutscenes = new();
}
