using UnityEngine;

[CreateAssetMenu(fileName = "BaseGameData", menuName = "ScriptableObjects/BaseGameData", order = 1)]
public class BaseGameData : ScriptableObject
{
    public PlayerBaseData playerBaseData;
    public WorldBaseData worldBaseData;
}
