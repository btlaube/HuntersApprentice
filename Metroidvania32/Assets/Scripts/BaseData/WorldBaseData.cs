using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "World/Base Data", fileName = "WorldBaseData")]
public class WorldBaseData : ScriptableObject
{
    public List<string> activatedFlags = new();
}
