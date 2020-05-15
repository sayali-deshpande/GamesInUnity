using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] background; // list of background need to be parallaxed
    public float smoothing = 1f;  // how much the parallax is going to be

    private float[] parallaxScale; // the proportion of the camera movement to move the background by
    private Transform cam;
    private Vector3 prevCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        prevCamPos = cam.position;

        parallaxScale = new float[background.Length];
        //assign the corresponding parallax scales
        for(int i = 0; i < background.Length; i++)
        {
            parallaxScale[i] = background[i].position.z*-1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //for each background
        for(int i = 0; i < background.Length; i++)
        {
            //parallax is the opossite of camera's movement as previous frame is multiplied by scale
            float parallax = (prevCamPos.x - cam.position.x) * parallaxScale[i];

            //set the target x pos
            float backgroundTargetXpos = background[i].position.x + parallax;

            //create target vector
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetXpos,
                                                      background[i].position.y,
                                                      background[i].position.z);
            //fade between current pos and target pos
            background[i].position = Vector3.Lerp(background[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        prevCamPos = cam.position;
    }
}
