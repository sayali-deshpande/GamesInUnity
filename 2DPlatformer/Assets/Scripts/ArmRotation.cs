using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public int rotationOffset = 90;
    // Update is called once per frame
    void Update()
    {
        // Find the difference between current arm position and mouse position
        Vector3 diffVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diffVector.Normalize();
        float rotationZ = Mathf.Atan2(diffVector.y, diffVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset) ; // rotate around z axis
    }
}
