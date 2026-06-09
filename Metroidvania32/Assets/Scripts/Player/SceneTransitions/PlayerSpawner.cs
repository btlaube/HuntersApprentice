using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        SpawnPoint[] spawns = FindObjectsOfType<SpawnPoint>();

        // Debug.Log($"Attemping spawn at: {playerStateManager.CurrentState.spawnPointData.spawnID}");
        foreach (SpawnPoint spawn in spawns)
        {
            // Debug.Log($"Found spawn point: {spawn.spawnID}");
            if (spawn.spawnID == SpawnManager.Instance.SpawnData.spawnID)
            {
                transform.position = spawn.transform.position;
                break;
            }
        }
    }

}
