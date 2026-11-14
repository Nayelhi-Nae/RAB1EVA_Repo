using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{

   private Vector3 spawnPoint;

    void Start()
    {
        
        spawnPoint = transform.position;
    }


    public void UpdateSpawnPoint(Vector3 newSpawnPosition)
    {
        spawnPoint = newSpawnPosition;
        Debug.Log("Checkpoint Updated! New spawn point set.");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("RespawnFall") || other.CompareTag("HellSoul") || other.CompareTag("RespawnWalls"))
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        transform.position = spawnPoint;
        
    }
}
