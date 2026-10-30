using UnityEngine;

public class EnemyLinear : MonoBehaviour
{
    [Header("Enemy Movement")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement Settings")]
    public float speed = 2f;
    public float waitAtPoints = 0.1f;

    private Vector3 targetPosition;
    private bool goingToPointB = true;
    private float waitTimer = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = pointB.position;
        goingToPointB = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            waitTimer = waitAtPoints;

            goingToPointB = !goingToPointB;
            targetPosition = goingToPointB ? pointB.position : pointA.position;
        }

        Debug.Log($"Enemy position: {transform.position}, Target position: {targetPosition}, Going to Point B: {goingToPointB}");
    }
}
