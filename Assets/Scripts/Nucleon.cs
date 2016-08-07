using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Nucleon : MonoBehaviour {

    public float attractionForce;
    public float explosionRadius = 500.0f;
    public float explosionPower = 100.0f;

    Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    
	void FixedUpdate () {
        body.AddForce(transform.localPosition * -attractionForce);
        body.AddExplosionForce(explosionPower, transform.localPosition, explosionRadius, 3.0F);
    }
}
