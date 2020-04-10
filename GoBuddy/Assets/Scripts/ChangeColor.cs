using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private float startDelay = 0.1f;
    private float intervalDelay = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("changeColorPeriodically", startDelay, intervalDelay);
    }

    void changeColorPeriodically()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value);
        this.GetComponent<MeshRenderer>().material.color = newColor;
    }
}
