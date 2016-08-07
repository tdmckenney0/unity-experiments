using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour {

    public GameObject cubePrefabVar;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        Instantiate(cubePrefabVar);
    }
}   
