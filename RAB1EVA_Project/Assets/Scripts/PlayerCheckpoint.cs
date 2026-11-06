using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{

    public GameObject checkPoint;
    Vector3 spawnPoint;

    void Start()
    {
        spawnPoint = transform.position;
    }

    public void OnTriggerEnter(Collider other) //acá pueden añadir otras cosas para respawnear por checkpoint si lo quieren usar
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            spawnPoint = checkPoint.transform.position;
        }
        if (other.gameObject.CompareTag("RespawnFall"))
        {
            gameObject.transform.position = spawnPoint;
        }
        if (other.gameObject.CompareTag("HellSoul"))
        {
            gameObject.transform.position = spawnPoint;
        }
        if (other.gameObject.CompareTag("RespawnWalls"))
        {
            gameObject.transform.position = spawnPoint;
        }
    }
}
