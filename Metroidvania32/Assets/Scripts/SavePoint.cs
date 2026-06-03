using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private SpawnPointData spawnData;

    private LevelLoader levelLoader;
    private SceneLoadPlayerSpawner playerSpawner;

    private void Start()
    {
        levelLoader = LevelLoader.instance;
        playerSpawner = SceneLoadPlayerSpawner.instance;
        // Debug.Log($"TransitionDetector: {playerSpawner}");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerSpawner.UpdatePlayerSpawnPoint(spawnData);
            GameManager.Instance.SaveCheckpoint();
            //GameManager.Instance.ActivateCheckpoint(spawnData);
        }        
    }
}
