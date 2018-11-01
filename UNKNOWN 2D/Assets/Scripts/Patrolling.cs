using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour {

    public GameObject[] waypoints;
    private Rigidbody2D rb2d;
    int waypointCounter = 0;
    float nextWaypointDistance = 0.2f;
    public float speed, rotationSpeed = 20f;
    public bool next = true;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
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
            if (waypointCounter == waypoints.Length)
                {
                    waypointCounter = 0;
                }
        }

    }
}
