using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneLoadPlayerSpawner : MonoBehaviour
{

    // [Header("References")]
    // [SerializeField] private PlayerSpawnPosition playerSpawnPosition;
    // [SerializeField] private PlayerStateManager playerStateManager;

    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     playerStateManager = FindObjectOfType<PlayerStateManager>();
    // }

    public static SceneLoadPlayerSpawner instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        
        // DontDestroyOnLoad(gameObject);
    }

    // public void SetPlayerDestinationAndLoadScene(Vector3 destination, int sceneToLoad)
    // {
    //     playerSpawnPosition.spawnPoint = destination;
    //     LevelLoader.instance.LoadScene(sceneToLoad);
    // }

    public void UpdatePlayerSpawnPointAndLoadScene(SpawnPointData spawnData)
    {
        UpdatePlayerSpawnPoint(spawnData);
        StartCoroutine(LoadSceneAfterFrame(spawnData));
    }

    public void UpdatePlayerSpawnPoint(SpawnPointData spawnData)
    {
        GameManager.Instance.playerStateManager.CurrentState.spawnPointData = spawnData;
        // Debug.Log($"Updated spawn point to: {spawnData.spawnID} for scene: {spawnData.sceneName}");
    }

    private IEnumerator LoadSceneAfterFrame(SpawnPointData spawnData)
    {
        yield return new WaitForSeconds(0.1f); // Wait for the end of the current frame
        LevelLoader.instance.LoadScene(spawnData.sceneName);
    }


}
