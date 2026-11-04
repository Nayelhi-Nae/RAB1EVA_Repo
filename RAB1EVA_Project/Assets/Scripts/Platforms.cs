using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;


public class Platforms : MonoBehaviour
{
    [Header("Configuracion de caida")]
    public float fallDalay = 1.0f; //Tiempo que tarda en caer 
    public float destroyDelay = 2.0f; // Tiempo que tarda en destruirse despues de caer

    private Rigidbody rb;
    private bool IsFalling =  false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // que la plataforma no tenga selecionado la gravedad para que no caiga
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsFalling) 
        {
            StartCoroutine(FallRoutine);
        }
    }

    private void StartCoroutine(Func<IEnumerable> fallRoutine)
    {
        throw new NotImplementedException();
    }

    IEnumerable FallRoutine()
    {
        IsFalling = true;
        Debug.Log("La plataforma va a caer");

        yield return new WaitForSeconds(fallDalay);

        rb.isKinematic = false;

    }

}
