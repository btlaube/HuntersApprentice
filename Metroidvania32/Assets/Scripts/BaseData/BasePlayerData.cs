using UnityEngine;

[CreateAssetMenu(menuName = "Player/Base Stats", fileName = "PlayerBaseStats")]
public class BasePlayerData : ScriptableObject
{
    public float startingMaxHealth = 100f;
    public float startingMoveSpeed = 15f;
    public SpawnPointData startingSpawnPoint;
}
