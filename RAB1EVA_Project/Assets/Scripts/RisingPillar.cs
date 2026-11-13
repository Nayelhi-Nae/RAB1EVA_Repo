using UnityEngine;

public class RisingPillar : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("How high the pillar will rise from its starting position.")]
    public float riseHeight = 5f;
    [Tooltip("Speed at which the pillar rises.")]
    public float moveSpeed = 3f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isRising = false;
    private bool hasRisen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0f, riseHeight, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRising && !hasRisen)
        {
            // Move smoothly toward the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the pillar has reached the destination
            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition; // Snap precisely to target
                hasRisen = true;
                isRising = false;
            }
        }
    }

    public void ActivatePillar()
    {
        if (!hasRisen)
        {
            isRising = true;
        }
    }
}
