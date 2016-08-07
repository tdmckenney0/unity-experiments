using UnityEngine;
using System.Collections;

public class SpawnBulletEverySecond : MonoBehaviour {

    public GameObject bulletTemplate;
    public float interval = 1f;

    // Use this for initialization
    void Start () {
        InvokeRepeating("spawnBullet", interval, interval);
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void spawnBullet()
    {
        Instantiate(bulletTemplate);
    }
}
