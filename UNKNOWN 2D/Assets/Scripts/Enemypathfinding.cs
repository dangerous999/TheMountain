﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class Enemypathfinding : MonoBehaviour {

    #region PRIVATE
    private Seeker seeker;
    private Rigidbody2D rb2d;
    private int currentWaypoint = 0;            // index of currentWaypoint
    private bool noPath = true;                 // true if no current path present
    #endregion

    #region PUBLIC
    public Transform target;                    // We will move towards this point
    public Path path;                           // Calculated path
    public GameObject[] waypoints;              // List with patrol points

    int waypointCounter = 0;                    // patrol point index within waypoints[]

    public float updateTime;                    // how many path updates per second do we want
    public float speed = 10f;                   // 
    public float nextWaypointDistance = 3f;     // how precise we want to be, the lower the value the closer we will get to the actual point
    public float rotationSpeed = 20f;           //
    public float angleModifier = -90f;          // ¯\_(ツ)_/¯

    public bool see = false;                    // whether or not the player is in range

    public ForceMode2D FM;                      // modes can be FORCE or IMPULSE
    #endregion

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Find path from me to player, shouldn't be used too many times a second, limited by updateTime
    IEnumerator UpdatePath()                    
    {
        while (see)
        {
            //Debug.Log("Pozvan UpdatePath");
            if (target == null)
            {
                //Debug.Log("nema targeta");
            }
            else
            {
                seeker.StartPath(transform.position, target.position, OnPathComplete);
            }
            //Debug.Log("NIG NIG");

            yield return new WaitForSeconds(1f / updateTime);
        
        }
    }
    // Find path to next waypoint, should only be called once per waypoint
    IEnumerator PatrolWaypointPath()          
    {
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        Debug.Log("Patrol Nigger");
        yield return new WaitForSeconds(2f / updateTime);
        StopCoroutine(PatrolWaypointPath());
    }
    // No fucking clue sokol exblain this shit
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (see) // Move towards the player if you can see him
        {
            #region ISeeThePlayer
            if (path == null)
                return;

            transform.up = (target.transform.position - transform.position).normalized;  // Ovo je zapravo rotacija
            MoveTowardsTarget(path.vectorPath[currentWaypoint]);

            if (ReachedWaypoint(path.vectorPath[currentWaypoint], nextWaypointDistance))
            {
                currentWaypoint++;
                return;
            }
            #endregion
        }
        else // can't see the player -> go back to patrolling
        {
            #region ICantSeeThePlayer
            if (noPath) // Only need to calculate path to next patrol point once
            {
                Debug.Log("napravljeno");
                target = waypoints[waypointCounter].transform;
                StartCoroutine(PatrolWaypointPath());
                noPath = false;
            }
            
            if (ReachedWaypoint(waypoints[waypointCounter].transform.position, nextWaypointDistance)) // If you reached patrolpoint increment waypointCounter to next waypoint
            {
                waypointCounter++;
                noPath = true;
                if (waypointCounter == waypoints.Length)
                {
                    waypointCounter = 0;                  
                }
            }
    
            MoveTowardsTarget(path.vectorPath[currentWaypoint]);
            RotateTowardsTarget2D(target.transform, -90f, rotationSpeed);

            if ( ReachedWaypoint(path.vectorPath[currentWaypoint], nextWaypointDistance)  )
            {
                currentWaypoint++;
                return;
            }
            #endregion
        }
    }
    // When player is inside the trigger he is our target and we can see him
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            target = GameObject.FindGameObjectWithTag("Player").transform;
            StartCoroutine(UpdatePath());
            see = true;
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            see = true;
            //StartCoroutine(UpdatePath());
        }
    }
    // When player leaves we can't see him anymore
    private void OnTriggerExit2D(Collider2D collision)
    {      
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Nigger 2");
            //StopCoroutine(UpdatePath());
            see = false;
            noPath = true;
            target = waypoints[waypointCounter].transform;            
        }
    }

    /// <summary>
    /// Calculates direction towards the target and adds force to RigidBody2D in that direction
    /// </summary>
    /// <param name="target"></param>
    private void MoveTowardsTarget(Vector3 target)
    {

        Vector3 dir = (target - transform.position).normalized;           // direkcija prema drugom waypointu
        dir *= speed;
        rb2d.AddForce(dir, FM);
    }
    /// <summary>
    /// Checks distance towards target and if it's smaller than the treshold return true else return false
    /// </summary>
    /// <param name="target"></param>
    /// <param name="distanceTreshold">How close we want to get to the target, smaller value -> closer</param>
    /// <returns></returns>
    private bool ReachedWaypoint(Vector3 target, float distanceTreshold)
    {
        float distanceToCurrentWaypoint = Vector3.Distance(transform.position, target);
        if (distanceToCurrentWaypoint < distanceTreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Rotates object towards target with a certain speed, angleModifier is there because ¯\_(ツ)_/¯
    /// </summary>
    /// <param name="target">Objekt prema kojemu se zelimo rotirati</param>
    /// <param name="angleModifier">Kod nas je -90f jer tako mora bit</param>
    /// <param name="rotationSpeed">rotation speed</param>
    private void RotateTowardsTarget2D(Transform target, float angleModifier, float rotationSpeed)
    {
        Vector2 dis = (target.transform.position - transform.position).normalized; // Smjer prema originalPositionu
        float angle = (Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg) + angleModifier;                                        //kut do objekta (-90 JE DA y gleda prema objektu nepitaj me zasto)
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);                                     //rotacija po z osi
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);    //sporo okretanje po z osi
    }

}
