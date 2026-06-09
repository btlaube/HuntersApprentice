using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{

    // [Header("References")]
    // [SerializeField] private PlayerStateManager stateManager;
    // private LevelLoader levelLoader;

    // private SceneLoadPlayerSpawner playerSpawner;

    void Start()
    {
        // levelLoader = LevelLoader.instance;
        // playerSpawner = SceneLoadPlayerSpawner.instance;
        // //TODO: Move input enabling to MasterInputHandler or PlayerStateManager
        // MasterInputHandler.Instance.playerInput.EnableInput();
    }

    public void OnPlayerDeath()
    {
        // playerComponents.stateManager.Respawn();
        // StartCoroutine(RespawnCoroutine());
        GameManager.Instance.OnPlayerDeath(transform.position);

    }

    // private IEnumerator RespawnCoroutine()
    // {
    //     //TODO: Move input enabling to MasterInputHandler or PlayerStateManager
    //     // MasterInputHandler.Instance.playerInput.DisableInput();
    //     GameStateManager.Instance.ChangeState(GameState.PlayerDead); // Ensure we're in the correct state for respawning
    //     // Wait for death animation to finish
    //     yield return new WaitForSeconds(1f);

    //     // stateManager.Respawn();
    //     // playerSpawner.UpdatePlayerSpawnPointAndLoadScene(stateManager.CurrentState.spawnPointData);
    //     // levelLoader.LoadScene(stateManager.CurrentState.spawnPointData.sceneName);
    //     // levelLoader.ResetScene();
    //     // playerSpawner.SetPlayerDestinationAndLoadScene(playerSpawner.playerSpawnPosition.spawnPoint, SceneManager.GetActiveScene().buildIndex);
    //     GameManager.Instance.Respawn();
    // }
}
