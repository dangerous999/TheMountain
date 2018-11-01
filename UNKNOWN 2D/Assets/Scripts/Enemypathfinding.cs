using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class Enemypathfinding : MonoBehaviour {

    public Transform target;
    private Seeker seeker;
    public float UpdateTime;                    //nakon koliko se sekundi updejta put
    private Rigidbody2D rb2d;

    public Path path;                           // KALKULIRANI PUT
    public float speed=10f;                     // brzina po sekundi
    public ForceMode2D FM;                      // promjena FORCE/INPULS
    public bool PathEnded;                      // je li doso do kraja puta 
    public bool see = false;                    // jel vidljiv player
    public bool hasPatrolRoute = true;
    public float nextWaypointDistance = 3f;     // udaljenost od waypointa na kojoj kreće na sljedeći waypoint
    private int currentWaypoint = 0;            // waypoint na kojeg idemo trenutno

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        //seeker.StartPath(transform.position, target.position, OnPathComplete);      //novi put do targeta i vraca put u OnPathComplete funkciju
        //StartCoroutine(UpdatePath());
    }

    // Nalazi put od neprijatelja do playera
    IEnumerator UpdatePath()                    
    {
        //while (see)
        //{
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

            yield return new WaitForSeconds(1f / UpdateTime);
        
        //}
    }

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
        
        StartCoroutine(UpdatePath());
        if (see)
        {
            if (path == null)
                return;
            #region PathEndedShit
        /* (currentWaypoint >= path.vectorPath.Count)       //ako je trenutni waypoint > broja waypointa
        {
            if (PathEnded)
                return;
            Debug.Log("end of path");
            PathEnded = true;
            return;

        }
        PathEnded = false;*/
        #endregion
            Debug.Log("NIGRS");
            transform.up = (target.transform.position - transform.position).normalized;                 // gleda playera
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;           // direkcija prema drugom waypointu
            dir *= speed;
            rb2d.AddForce(dir, FM);
            float dis = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);         // Udaljenost do sljedećeg waypointa
            if (dis < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }
        }
        else
        {
            //StartCoroutine(UpdatePath());
            if (path == null)
                return;
            transform.up = (target.transform.position - transform.position).normalized;                 // gleda playera
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;           // direkcija prema drugom waypointu
            dir *= speed;
            rb2d.AddForce(dir, FM);
            float dis = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);         // Udaljenost do sljedećeg waypointa
            if (dis < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }

        }
    }
    // za triggere
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Nigger 2");
            //StopCoroutine(UpdatePath());
            see = false;
        }
    }
}
