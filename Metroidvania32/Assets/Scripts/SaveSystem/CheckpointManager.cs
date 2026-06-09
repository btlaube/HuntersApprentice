using UnityEngine;

[System.Serializable]
public class CheckpointSnapshot
{
    public int currency;
}

public class CheckpointManager : MonoBehaviour
{

    public GameObject checkpointObjectPrefab;

    private CheckpointSnapshot checkpointSnapshot = new CheckpointSnapshot {currency = 0};

    public static CheckpointManager Instance { get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SaveCheckpoint()
    {
        checkpointSnapshot.currency = PlayerDataManager.Instance.Data.currency;
    }
    public void RestoreCheckpoint()
    {
        PlayerDataManager.Instance.Data.currency = checkpointSnapshot.currency;
    }

    public void OnPlayerDeath(Vector2 playerDeathPosition)
    {
        RestoreCheckpoint();
        SpawnCheckpointObject(playerDeathPosition);
    }

    private void SpawnCheckpointObject(Vector2 playerDeathPosition)
    {
        GameObject checkpointObject = Instantiate(checkpointObjectPrefab, playerDeathPosition, Quaternion.identity);   
        // checkpointObject.GetComponent<CheckpointObjectBehavior>.dropCurrency = checkpointSnapshot.currency;
    }

}