using UnityEngine;
using System.Collections;

public class Boid : MonoBehaviour {

    [Header("Boid Traits")]
    public Vector3 anchor;
    public int turnSpeed = 2;
    public int maxSpeed = 25;
    public float cohesionRadius = 10f;
    [Tooltip("The # of nearby boids we'll compare to")]
    public int maxBoids = 10;

    [Header("-----")]
    public float separationDistance = 10f;
    public float cohesionCoefficient = 1f;
    public float alignmentCoefficient = 4f;
    public float separationCoefficient = 2f;
    public float tick = 15f;

    [Header("-----")]
    public float boundaryDistance = 25f;
    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public Transform tr;

    private Collider[] boids;
    private Vector3 cohesion;
    private Vector3 separation;
    private int separationCount;
    private Vector3 alignment;

    private Boid b;
    private Vector3 vector;
    private int i;

    void Awake()
    {
        // Initialize transform shortcut, velocity. //
        tr = transform;
        velocity = Random.onUnitSphere * maxSpeed;
    }

    // Use this for initialization
    private void Start ()
    {
        InvokeRepeating("CalculateVelocity", Random.value * tick, tick);
        InvokeRepeating("UpdateRotation", Random.value, 0.1f);

	}

    void UpdateRotation()
    {
        if(velocity != Vector3.zero && transform.forward != velocity.normalized)
        {
            transform.forward = Vector3.RotateTowards(transform.forward, velocity, turnSpeed, 1);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Vector3.Distance(tr.position, anchor) > boundaryDistance)
        {
            velocity += (anchor - tr.position) / boundaryDistance;
        }

        tr.position += velocity * Time.deltaTime;
	}

    void CalculateVelocity()
    {
        boids = Physics.OverlapSphere(tr.position, cohesionRadius);

        if (boids.Length < 2) return;

        velocity = Vector3.zero;
        cohesion = Vector3.zero;
        separation = Vector3.zero;
        separationCount = 0;
        alignment = Vector3.zero;

        // flock separation

        for(i = 0; i < boids.Length && i < maxBoids; i++)
        {
            b = boids[i].GetComponent<Boid>();
            cohesion += b.tr.position;
            alignment += b.velocity;
            vector = tr.position - b.tr.position;
            if(vector.sqrMagnitude > 0 && vector.sqrMagnitude < separationDistance * separationDistance)
            {
                separation += vector / separationDistance * separationDistance;
                separationCount++;
            }
        }

        // Flock Cohesion

        cohesion = cohesion / (boids.Length > maxBoids ? maxBoids : boids.Length);
        cohesion = Vector3.ClampMagnitude(cohesion - tr.position, maxSpeed);
        cohesion *= cohesionCoefficient;
        
        if(separationCount > 0)
        {
            separation = separation / separationCount;
            separation = Vector3.ClampMagnitude(separation, maxSpeed);
            separation *= separationCoefficient;
        }

        alignment = alignment / (boids.Length > maxBoids ? maxBoids : boids.Length);
        alignment = Vector3.ClampMagnitude(alignment, maxSpeed);
        alignment *= alignmentCoefficient;

        velocity = Vector3.ClampMagnitude(cohesion + separation + alignment, maxSpeed);
    }
}
