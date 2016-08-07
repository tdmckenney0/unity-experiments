using UnityEngine;
using System.Collections;

public class HelloWorldCameraController : MonoBehaviour {

	void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        transform.position = transform.position + movement; 
    }
}
