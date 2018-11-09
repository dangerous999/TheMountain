using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour {

    public GameObject[] waypoints;
    private Rigidbody2D rb2d;
    private Enemypathfinding enemyPathFindingScript;
    private GameObject player;
    int waypointCounter = 0;
    float nextWaypointDistance = 0.2f;
    public float speed, rotationSpeed = 20f;

    public bool next = true;

    public bool isOnPatrol = true;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        enemyPathFindingScript = GetComponent<Enemypathfinding>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 dis = (waypoints[waypointCounter].transform.position - transform.position).normalized;          
        float angle = (Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg) - 90f;                                        //kut do objekta (-90 JE DA y gleda prema objektu nepitaj me zasto)
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);                                     //rotacija po z osi
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);    //sporo okretanje po z osi
    }
    void FixedUpdate()
    {
        if (next)
        {

            next = false;
        }

        //Vector3 dir = (waypoints[waypointCounter].transform.position - transform.position).normalized;
        //rb2d.AddForce(dir * speed);
        float distance = Vector3.Distance(transform.position, waypoints[waypointCounter].transform.position);
        if (distance < nextWaypointDistance)
        {
            waypointCounter++;
            next = true;
            if (waypointCounter == waypoints.Length) ;

            /*if (!enemyPathFindingScript.see)
            {         
                if (isOnPatrol)
                {
                    enemyPathFindingScript.target = waypoints[waypointCounter].transform;
                    Vector2 dis = (waypoints[waypointCounter].transform.position - transform.position).normalized;
                    float angle = (Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg) - 90f;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                enemyPathFindingScript.target = player.transform;
            }*/


        }
    }
    /*void FixedUpdate()
    {
        if (!enemyPathFindingScript.see)
        {
            if (isOnPatrol)
            {
                enemyPathFindingScript.target = waypoints[waypointCounter].transform;
                Vector2 dis = (waypoints[waypointCounter].transform.position - transform.position).normalized;
                float angle = (Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg) - 90f;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                //////////////
                /*Vector3 dir = (waypoints[waypointCounter].transform.position - transform.position).normalized;
                rb2d.AddForce(dir * enemyPathFindingScript.speed);
                float distance = Vector3.Distance(transform.position, waypoints[waypointCounter].transform.position);
                if (distance < nextWaypointDistance)

                {
                    Debug.Log("BIG FAT WIGGER (alcor)");
                    waypointCounter++;
                    enemyPathFindingScript.target = waypoints[waypointCounter].transform;
                    if (waypointCounter == waypoints.Length)
                    {
                        waypointCounter = 0;
                    }
                }
            }
        }
        else
        {
            enemyPathFindingScript.target = player.transform;
        }


    }*/
}
