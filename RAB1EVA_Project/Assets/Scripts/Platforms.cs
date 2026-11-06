using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Configuración de caída")]
    [Tooltip("Tiempo de espera en segundos antes de que la plataforma empiece a caer.")]
    public float fallDelay = 1.0f;

    [Tooltip("Tiempo que tarda la plataforma en destruirse tras empezar a caer (opcional).")]
    public float destroyDelay = 3.0f;

    private Rigidbody rb;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Detecta cuando el jugador colisiona con la plataforma
    private void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si el objeto que chocó tiene la etiqueta "Player"
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            StartCoroutine(FallRoutine());
        }
    }

    IEnumerator FallRoutine()
    {
        isFalling = true;

        //  Aquí puedes añadir efectos visuales o de sacudida (shake) antes de caer

        // Espera el tiempo configurado
        yield return new WaitForSeconds(fallDelay);

        // Activamos las físicas de caída
        rb.isKinematic = false; // Permite que las físicas actúen sobre el objeto
        rb.useGravity = true;   // Hace que la gravedad tire de la plataforma hacia abajo

        // Opcional: Destruir la plataforma después de un tiempo para limpiar la escena
        Destroy(gameObject, destroyDelay);
    }
}


