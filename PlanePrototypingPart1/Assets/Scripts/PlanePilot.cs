using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour
{
    public Vector3 pos;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Plane pilot script is attached to :: " + gameObject.name);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //Make camera follow the plane
        Vector3 desiredPos = -transform.right;
        float bias = 0.96f;
        Vector3 moveCamTo = transform.position - desiredPos * 10.0f + Vector3.up * 5.0f;
        //Vector3 moveCamTo = transform.position - camOffset;
        Camera.main.transform.position = Camera.main.transform.position * bias +
                                            moveCamTo * (1.0f - bias);
        Camera.main.transform.LookAt(transform.position + desiredPos * 10.0f);
    }
    void Update()
    {
        transform.position += (-transform.right) * Time.deltaTime * speed; //* 90.0f;
        speed -= transform.right.y * Time.deltaTime * 5.0f;
        if(speed < 10.0f)
        {
            speed = 10.0f;
        }
        transform.Rotate(-Input.GetAxis("Horizontal"), 0.0f, -Input.GetAxis("Vertical"));
        pos = transform.position;
        float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if(terrainHeightWhereWeAre > transform.position.y)
        {
            // it will come here when plane collides with mountain or surface
            //explode or crash the plane here
            transform.position = new Vector3(transform.position.x,
                                             terrainHeightWhereWeAre,
                                             transform.position.z);
        }
    }
}
