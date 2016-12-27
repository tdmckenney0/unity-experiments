using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour {

    private Rigidbody rb;

    public float speed = 1;
    public float accel = 1; 

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float throttle = 0.0f;

        if(Input.GetButtonDown("Jump"))
        {
            throttle += accel; 
        }

        if(Input.GetButtonDown("Fire1"))
        {
            throttle -= accel;
        }

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, throttle);

        rb.AddForce(movement * speed);
    }
}
