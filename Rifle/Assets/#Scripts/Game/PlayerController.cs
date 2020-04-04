using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody rb;

    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        velocity = direction * speed;
    }

    public void LookAt(Vector3 point)
    {

        //Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
        //transform.LookAt(heightCorrectedPoint);
        transform.LookAt(transform.position + point);
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + (velocity * Time.fixedDeltaTime));
        

        
        
        rb.MovePosition(rb.position + (velocity * Time.fixedDeltaTime));


    }

    
}
 