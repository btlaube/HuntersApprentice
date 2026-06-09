using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDetector : MonoBehaviour
{
    [SerializeField] private SpawnPointData spawnData;
    public float triggerDirection;

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
                GameManager.Instance.OnSceneTransitionTrigger(spawnData);
                Debug.Log($"Transitioning to scene: {spawnData.sceneName} with spawn point: {spawnData.spawnID}");
            }
            else
            {
                // Debug.Log("Wrong way");
            }
        }        
    }
}
