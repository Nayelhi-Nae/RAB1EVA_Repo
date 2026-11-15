using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigoZonaSimple : MonoBehaviour
{
    [Header("Configuración")]
    public Transform jugador;           // Arrastra aquí a tu jugador
    public float velocidad = 4f;        // Qué tan rápido te persigue

    private bool jugadorEnLaZona = false; // Controla si estás dentro de su área

    void Start()
    {
        // Si no asignaste el jugador, lo busca solo por la etiqueta "Player"
        if (jugador == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                jugador = playerObj.transform;
            }
        }
    }

    void Update()
    {
        // Si el jugador está dentro de su zona, lo persigue
        if (jugadorEnLaZona && jugador != null)
        {
            // Mueve al enemigo hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);

            // Hace que mire al jugador
            transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));
        }
    }

    // --- ENTRA A LA ZONA: TE EMPIEZA A PERSEGUIR ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnLaZona = true;
        }
    }

    // --- SALE DE LA ZONA: SE DETIENE Y YA NO TE PERSIGUE MÁS ---
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnLaZona = false;
        }
    }

    // --- MATA AL JUGADOR SI LO TOCA ---
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Te ha tocado y has muerto!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
    
