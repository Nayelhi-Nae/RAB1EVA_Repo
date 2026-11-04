using UnityEngine;

public class BigPumpkin : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float acceleration = 0.5f;
    public float maxSpeed = 10f;
    public float turnSpeed = 2f;
    public float rollSpeed = 2f;

    private Rigidbody rb;
    private float currentSpeed;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null)
        {
            player = player.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, maxSpeed);

        Vector3 direction = (player.position - transform.position).normalized;

        Vector3 move = direction * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        Vector3 rollAxis = Vector3.Cross(Vector3.up, direction).normalized;
        rb .MoveRotation(Quaternion.LookRotation(direction) * Quaternion.AngleAxis(rollSpeed * Time.fixedDeltaTime, rollAxis));
    }

    // Update is called once per frame
    void Update()
    {

    }
}


  
