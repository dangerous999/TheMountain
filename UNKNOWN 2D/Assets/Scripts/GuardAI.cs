using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class GuardAI : MonoBehaviour {

    #region PRIVATE
    private Seeker seeker;
    private Rigidbody2D rb2d;
    private int currentWaypoint = 0;            // index of currentWaypoint
    private bool noPath = true;                 // true if no current path present
    #endregion

    #region PUBLIC
    public Transform target;                    // We will move towards this point
    public Transform originalPosition;          // Spawn point
    public Path path;                           // Calculated path

    public float updateTime;                    // how many path updates per second do we want
    public float speed = 10f;                   // 
    public float nextWaypointDistance = 3f;     // how precise we want to be, the lower the value the closer we will get to the actual point
    public float rotationSpeed = 20f;           //
    public float angleModifier = -90f;          // ¯\_(ツ)_/¯

    public bool see = false;                    // whether or not the player is in range
    public bool isAtHome = true;                // is true when we are at spawnpoint

    public ForceMode2D FM;                      // modes can be FORCE or IMPULSE
    #endregion

    // Use this for initialization
    void Start () {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        //originalPosition = this.transform; // Ne vraća se ovoj poziciji (dodali smo empty gameobject da popravimo) nakon što ne vidi playera, TODO zašto?
    }

    // Find path from me to player, shouldn't be used too many times a second, limited by updateTime
    IEnumerator UpdatePath()
    {
        while (see)
        {
            if (target == null)
            {
                Debug.Log("nema targeta");
            }
            else
            {
                seeker.StartPath(transform.position, target.position, OnPathComplete);
            }

            yield return new WaitForSeconds(1f / updateTime);

        }
    }
    // Find path to next waypoint, should only be called once per waypoint
    IEnumerator PatrolWaypointPath()
    {
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateTime);
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

    void FixedUpdate()
    {
              
        if (see) // Move towards the player if you can see him
        {
            #region ISeeThePlayer
            if (path == null)
                return;

            transform.up = (target.transform.position - transform.position).normalized;  // Ovo je zapravo rotacija
            MoveTowardsTarget(path.vectorPath[currentWaypoint]);

            if (ReachedWaypoint (path.vectorPath[currentWaypoint], nextWaypointDistance) )
            {
                currentWaypoint++;
                return;
            }

            isAtHome = false; // Prati playera znaci da nije doma
            #endregion
        }
        else  // can't see player -> go back to your original post and stay there
        {
            #region ICantSeeThePlayer
            if (!isAtHome)   // If you're not home find best path and go home
            {
                if (noPath) // Only need to calculate path to home once
                {
                    target = originalPosition;
                    StartCoroutine(PatrolWaypointPath());
                    noPath = false;
                }

                if (ReachedWaypoint(target.transform.position, nextWaypointDistance)) // If you reached home stop moving and TODO go back to your original rotation
                {
                    Debug.Log("I'm home master");
                    transform.rotation = Quaternion.Euler(-180, 0, 0);                   //TODO set rotation to original
                    isAtHome = true;
                }
                else { // I'm not home yet -> rotate and move to home using the best path
                    Debug.Log("Time to move some cotton");
                    // rotacija
                    RotateTowardsTarget2D(originalPosition.transform, angleModifier, rotationSpeed);                 
                    MoveTowardsTarget(path.vectorPath[currentWaypoint]);     
                                                    
                    if (ReachedWaypoint(path.vectorPath[currentWaypoint], nextWaypointDistance))
                    {
                        currentWaypoint++;
                        return;
                    }

                }
            }
            #endregion
        }
    }

    // When player is inside the trigger he is our target and we can see him
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            StartCoroutine(UpdatePath());
            see = true;           
        }
    }
    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            see = true;
        }
    }
    // When player leaves we can't see him anymore
    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Nigger 2");
            see = false;
            noPath = true; 
            StopCoroutine(PatrolWaypointPath());
            isAtHome = false;
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
