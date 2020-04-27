using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 _camOffset = new Vector3(2.05f, 4.20f, -11.41f);
  
    void Update()
    {
        transform.position = player.transform.position + _camOffset;
    }
}
