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
    private Patrolling patrollingScript;

    //patroliranje
    public GameObject[] waypoints;
    int waypointCounter = 0;
    public float rotationSpeed = 20f;


    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        patrollingScript = GetComponent<Patrolling>();
        //seeker.StartPath(transform.position, target.position, OnPathComplete);      //novi put do targeta i vraca put u OnPathComplete funkciju
        //StartCoroutine(UpdatePath());
    }

    // Nalazi put od neprijatelja do playera
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

            yield return new WaitForSeconds(1f / UpdateTime);

        }
    }

    IEnumerator UpdateWaypointPath()
    {
        while (!see)
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
            
            yield return new WaitForSeconds(2f / UpdateTime);

        }
        
        
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
            
            transform.up = (target.transform.position - transform.position).normalized;                 // gleda playera
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;           // direkcija prema drugom waypointu
            dir *= speed ;
            rb2d.AddForce(dir, FM);
            float dis = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);         // Udaljenost do sljedećeg waypointa
            if (dis < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }
        }else 
        {
            if (hasPatrolRoute)
            {
                Debug.Log("napravljeno");
                target = waypoints[waypointCounter].transform;
                StartCoroutine(UpdateWaypointPath());
                hasPatrolRoute = false;
            }
            
            
            Vector2 dis = (target.transform.position - transform.position).normalized;
            float angle = (Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg) - 90f;                                        //kut do objekta (-90 JE DA y gleda prema objektu nepitaj me zasto)
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);                                     //rotacija po z osi
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);    //sporo okretanje po z osi
            float distance = Vector3.Distance(transform.position, waypoints[waypointCounter].transform.position);

            Debug.Log(distance);
            if (distance < nextWaypointDistance)
            {
                Debug.Log("Nigger");
                waypointCounter++;
                hasPatrolRoute = true;
                if (waypointCounter == waypoints.Length)
                {
                    waypointCounter = 0;
                }
            }
            // pomicanje
            
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;           // direkcija prema drugom waypointu
            dir *= speed;
            rb2d.AddForce(dir, FM);
            float dis2 = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);         // Udaljenost do sljedećeg waypointa
            if (dis2 < nextWaypointDistance)
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Nigger 2");
            StopCoroutine(UpdatePath());
            see = false;
            target= waypoints[waypointCounter].transform;
            hasPatrolRoute = true;
        }
    }
}
