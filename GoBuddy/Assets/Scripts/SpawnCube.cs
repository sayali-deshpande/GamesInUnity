using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public float startDelay = 0.1f;
    public float internalDelay = 0.01f;
   
    public GameObject myCube;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnCubes", startDelay, internalDelay);
    }

    void spawnCubes()
    {
        Instantiate(myCube, new Vector3(Random.Range(-6, 6), 10, 0), Quaternion.identity);
    }
}
