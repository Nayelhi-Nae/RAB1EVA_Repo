using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //Librería de carga/descarga de escenas
using System;

public class PlayerInteractor : MonoBehaviour
{
    public static event Action<int, int> OnPointsChanged;

    [Header("Respawn Configuration")]
    public Transform respawnPoint; //Ref al punto de respawn para usar su posición
    public float respawnFallLimit = -10; //Valor negativo en eje Y a partir del cual se respawnea

    [Header("Point System")]
    public int points; //Cantidad de puntos actuales del player
    public int winPoints = 5; //Cantidad de puntos necesarios para ganar

    [Header("Reference Variables")]
    public Rigidbody playerRb; //Ref al rigidbody para resetear su motor de aceleración al respawnear

    [Header("SFX Configuration")]
    public AudioClip[] sfxList; //Array de sonidos: diferentes sonidos agrupados para diferentes acciones
    public AudioSource sfxSource; //Ref al altavoz del player: referenciarlo permite reproducir sonidos cuando queramos

    [Header("Scene Management")]
    public bool levelCompleted = false; //Bool que determina si se ha completado el nivel


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points = 0; //Cuando empieza la partida, los puntos se ponen solos en cero
        levelCompleted = false;

        winPoints = GameObject.FindGameObjectsWithTag("PickUp").Length;

        if (playerRb == null )
        {
            playerRb = GetComponent<Rigidbody>();
        }
        if (respawnPoint == null)
        {
            respawnPoint = transform;
        }
        OnPointsChanged?.Invoke(points, winPoints);
    }

    // Update is called once per frame
    void Update()
    {
        //Respawn por caída
        if (transform.position.y <= respawnFallLimit) 
        { 
            Respawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayerDied();
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PickUp"))
        {
            AddPickup();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            PlayerDied();
        }
        else if (other.gameObject.CompareTag("Switch"))
        {
            sfxSource.PlayOneShot(sfxList[3]); //Reproducir el sonido asociado al switch
        }
        
    }

    public void AddPickup()
    {
        if (levelCompleted) return;

        points++;

        if (sfxSource != null && sfxList.Length > 2)
        {
            sfxSource.PlayOneShot(sfxList[2]);
        }
        OnPointsChanged?.Invoke(points, winPoints);

        if (points >= winPoints)
        {
            LevelCleared();
        }
    }

    void LevelCleared()
    {
        levelCompleted = true;

        if (SceneManagement.Instance != null)
        {
            SceneManagement.Instance.LevelCompleted(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Respawn()
    {
        //1- Cambia la posición del player por la del punto de respawn
        transform.position = respawnPoint.position;
        //2- Resetea la velocidad del Rigidbody para evitar acumulaciones de aceleración
        playerRb.linearVelocity = new Vector3(0, 0, 0);
        //3- Resetea la velocidad de rotación
        playerRb.angularVelocity = new Vector3(0, 0, 0);
        //4- Reproducir el sonido de respawn desde el audiosource del player
        sfxSource.PlayOneShot(sfxList[1]);
    }

    public void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
