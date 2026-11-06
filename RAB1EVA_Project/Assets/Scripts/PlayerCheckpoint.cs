using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{

    public GameObject checkPoint;
    Vector3 spawnPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            spawnPoint = checkPoint.transform.position;
        }
        if (other.gameObject.CompareTag("RespawnFall"))
        {
            gameObject.transform.position = spawnPoint;
        }
    }
}
