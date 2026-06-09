using UnityEngine;

[CreateAssetMenu(fileName = "BaseGameData", menuName = "ScriptableObjects/BaseGameData", order = 1)]
public class BaseGameData : ScriptableObject
{
    public BasePlayerData basePlayerData;
    public BaseWorldData baseWorldData;
}
