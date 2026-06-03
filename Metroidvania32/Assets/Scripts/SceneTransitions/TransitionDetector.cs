using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDetector : MonoBehaviour
{
    [SerializeField] private SpawnPointData spawnData;

    // public int sceneToLoad;
    // public Vector3 playerDestination;
    public float triggerDirection;

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
        // Debug.Log("Fart!");
        if (other.tag == "Player")
        {
            // Check trigger direction 
            Vector3 diff = other.transform.position - transform.position;
            // Debug.Log(diff.x);
            if (diff.x * triggerDirection > 0.0f)
            {
                // Debug.Log("Change Scene");
                // transManager.playerDestination = playerDestination;
                // other.GetComponent<PlayerInputHandler>().DisableInput();
                // TODO: Move input enabling to MasterInputHandler or PlayerStateManager
                // playerSpawner.SetPlayerDestinationAndLoadScene(playerDestination, sceneToLoad);
                // levelLoader.LoadScene(sceneToLoad);

                // TODO: Change to transition detector event received by GameManager
                // MasterInputHandler.Instance.playerInput.DisableInput();
                GameStateManager.Instance.ChangeState(GameState.Loading);
                playerSpawner.UpdatePlayerSpawnPointAndLoadScene(spawnData);
                Debug.Log($"Transitioning to scene: {spawnData.sceneName} with spawn point: {spawnData.spawnID}");
            }
            else
            {
                // Debug.Log("Wrong way");
            }
        }        
    }
}
