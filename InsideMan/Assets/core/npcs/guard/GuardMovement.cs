using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    // public variables
    public GameObject[] patrolPoints;   // List of the guard's patrol points
    public float moveSpeed;             // The guard's movement speed
    public float collisionDelay;        // Delay after the guard collides with the patrol point
    public float waitTime;              // Time the guard waits before proceeding to the next patrol point

    // private variables
    GameObject currentPoint;    // The point that the guard is currently moving towards
    int currentPointIndex;      // Index in patrolPoints for the currentPoint

	// Use this for initialization
	void Start ()
    {
        currentPoint = patrolPoints[0];
        currentPointIndex = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If the currentPoint is valid, move towards it
		if(currentPoint)
        {
            transform.Translate(Utilities2D.GetDirection3D(transform.position, currentPoint.transform.position) * Time.deltaTime * moveSpeed);
        }
	}

    // When the guard reaches its currentPoint, get the next point in the patrol
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Patrol_Point")
        {
            StartCoroutine(DelayAfterColliding(collisionDelay));
        }
    }

    // Short delay after colliding with patrol point
    IEnumerator DelayAfterColliding(float collDelay)
    {
        // Get ready to move to the next point after collDelay seconds
        yield return new WaitForSeconds(collDelay);

        StartCoroutine(WaitBeforePatrolling(waitTime));
    }

    // Wait for waitTime seconds, then get the next patrol point
    IEnumerator WaitBeforePatrolling(float waitTime)
    {
        currentPoint = null;
        yield return new WaitForSeconds(waitTime);

        if (currentPointIndex < patrolPoints.Length - 1)
        {
            currentPoint = patrolPoints[++currentPointIndex];
            Debug.Log(currentPointIndex);
        }
        else
        {
            currentPoint = patrolPoints[0];
            currentPointIndex = 0;
        }
    }
}
