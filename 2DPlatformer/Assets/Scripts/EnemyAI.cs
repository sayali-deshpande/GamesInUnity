using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    //AI to chase
    public Transform target;
    //Time per second will update our path
    public float updateRate = 2f;
    //caching
    private Seeker seeker;
    private Rigidbody2D rb;

    //The calculated path
    public Path path;
    //The AI's speed per second
    public float speed = 300f;
    public ForceMode2D forcemode;

    [HideInInspector]
    public bool isPathEnded = false;

    //The max distance from AI to waypoint for it to continue to next waypoint
    public float nextWayPointDistance = 3;
    //The waypoint we are currently moving towards
    private int currentWayPoint;
    private bool searchForPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            if(!searchForPlayer)
            {
                searchForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }
        //start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }

    IEnumerator SearchForPlayer()
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        if (sResult != null)
        {
            target = sResult.transform;
            searchForPlayer = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }
    }

    IEnumerator UpdatePath()
    {
        if(target == null)
        {
            if (!searchForPlayer)
            {
                searchForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return false;
        }

        //start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }
    public void OnPathComplete(Path p)
    {
        Debug.Log("Does Path have any error: " + p.error);
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            if (!searchForPlayer)
            {
                searchForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        //ToDo: Always look at player
        if (path == null)
            return;

        if(currentWayPoint >= path.vectorPath.Count)
        {
            if (isPathEnded)
                return;

            Debug.Log("End of path is reached");
            isPathEnded = true;
            return;
        }
        isPathEnded = false;
        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir, forcemode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if(dist < nextWayPointDistance)
        {
            currentWayPoint++;
            return;
        }
    }
  
}
