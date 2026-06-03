using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;

    void Start()
    {
        // transform.position = playerSpawnPosition.spawnPoint;
        // Debug.Log($"Player spawned at: {transform.position}");
        // TODO: Remove and give responsibility to GameManager
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        SpawnPoint[] spawns = FindObjectsOfType<SpawnPoint>();

        // Debug.Log($"Attemping spawn at: {playerStateManager.CurrentState.spawnPointData.spawnID}");
        foreach (SpawnPoint spawn in spawns)
        {
            // Debug.Log($"Found spawn point: {spawn.spawnID}");
            if (spawn.spawnID == GameManager.Instance.playerStateManager.CurrentState.spawnPointData.spawnID)
            {
                // GameObject player = Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
                // player.transform.position = spawn.transform.position;
                transform.position = spawn.transform.position;
                // Debug.Log($"Player spawned at: {player.transform.position}");
                break;
            }
        }
    }

}
