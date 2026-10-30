using UnityEngine;

public class PickUp_one : MonoBehaviour
{
    [Header("Rotacion parameters")]
    public float speedX = 20;
    public float speedY = 30;
    public float speedZ = 40;
   

   

    // Update is called once per frame
    void Update()
    {
        RotateObject();
    }



    void RotateObject() 
    {
        transform.Rotate(Vector3.right * speedX * Time.deltaTime);
        transform.Rotate(Vector3.right * speedY * Time.deltaTime);
        transform.Rotate(Vector3.right * speedZ * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {

        }
    }

}
