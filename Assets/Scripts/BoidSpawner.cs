using UnityEngine;
using System.Collections;

public class BoidSpawner : MonoBehaviour {

    public Boid boidPrefab;
    public float timeBetweenSpawns = 2;
    public float spawnDistance = 25;

    float timeSinceLastSpawn;

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= timeBetweenSpawns)
        {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnNucleon();
        }
    }

    void SpawnNucleon()
    {
        Boid spawn = Instantiate<Boid>(boidPrefab);
        spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
    }
}
