using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public SpawnPointData SpawnData {get; private set;}

    public static SpawnManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void SetSpawnLocation(SpawnPointData spawnData)
    {
        SpawnData = spawnData;
    }

}