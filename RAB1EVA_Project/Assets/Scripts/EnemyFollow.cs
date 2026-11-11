using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class E : MonoBehaviour
{
   [Header("Base")]
    public Transform Object;
    public float Velocity;
    public NavMeshAgent IA;

    // Update is called once per frame
    void Update()
    {
        IA.speed = Velocity;
        IA.SetDestination(Object.position);
    }
}
