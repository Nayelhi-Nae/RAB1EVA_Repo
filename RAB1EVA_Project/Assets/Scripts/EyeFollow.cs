using UnityEngine;
using System.Collections;

public class EyeFollow : MonoBehaviour
{
  public Transform eyeTarget;
    public float rotationSpeed = 5f;

    void Update()
    {
        if (eyeTarget != null)
        {
            Vector3 direction = (eyeTarget.position - transform.position).normalized;
            
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }


}
