using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    private float yRange = 6.0f;
   
    // Update is called once per frame
    void Update()
    {
       if(transform.position.y < -yRange)
        {
            Destroy(gameObject);
        }
    }
}
