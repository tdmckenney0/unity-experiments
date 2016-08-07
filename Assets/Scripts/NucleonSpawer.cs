﻿using UnityEngine;
using System.Collections;

public class NucleonSpawer : MonoBehaviour {

    public float timeBetweenSpawns;
    public float spawnDistance;
    public Nucleon[] nucleonPrefabs;

    float timeSinceLastSpawn;
	
	// Update is called once per frame
	void FixedUpdate () {

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= timeBetweenSpawns)
        {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnNucleon();
        }
	}

    void SpawnNucleon()
    {
        Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);
        spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
    }
}
