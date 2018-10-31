using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour {

    public GameObject[] waypoints;
    private Rigidbody2D rb2d;
    int waypointCounter = 0;
    float nextWaypointDistance = 0.2f;
    public float speed, rotationSpeed;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {

        //foreach (GameObject waypoint in waypoints )
        //{
            //transform.up = (waypoints[waypointCounter].transform.position - transform.position).normalized;               //gleda WAYPOINT
            Vector3 dir = (waypoints[waypointCounter].transform.position - transform.position).normalized;
            rb2d.AddForce(dir * speed);
            float distance = Vector3.Distance(transform.position, waypoints[waypointCounter].transform.position);
        Vector3 dir2 = (new Vector3(0,0, waypoints[(waypointCounter + 1) % (waypoints.Length - 1)].transform.position.z - transform.position.z).normalized);
        Quaternion rotation = Quaternion.LookRotation(dir2);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        //Debug.Log(Vector3.Angle(transform.up, dir));
        if (distance < nextWaypointDistance)
            {
                
                waypointCounter++;
            if (waypointCounter == waypoints.Length)
                {
                    waypointCounter = 0;
                }

            

            //Quaternion rotation = Quaternion.LookRotation(0, 0, transform.rotation - dir));
            //transform.rotation = rotation;
            //transform.rotation = rotation;
            //Vector3.Angle(transform.up, dir);
            //Debug.Log(Vector3.Angle(transform.up, dir));
            //Quaternion.Slerp(this.gameObject.transform.rotation, new Vector3.Angle(transform.up, dir) as Quaternion, Time.deltaTime * rotationSpeed);
            //Quaternion.Slerp(this.gameObject.transform.rotation, waypoints[waypointCounter].transform.rotation, speed);

            //Vector3 dir2 = (waypoints[waypointCounter].transform.position - transform.position).normalized;
            //Quaternion rotation = Quaternion.LookRotation(dir2);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        }
        //transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, waypoints[waypointCounter].transform.rotation, speed);



        /*float angle = Quaternion.Angle(this.gameObject.transform.rotation, head.transform.rotation);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.rotation = head.transform.rotation;

        }
        else if (angle > 60)
        {
            transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, head.transform.rotation, speed);
        




        //}
        /*
            transform.up = (target.transform.position - transform.position).normalized;                 //gleda playera
            //Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;           //direkcija prema drugom waypointu
            dir *= speed;
            rb2d.AddForce(dir, FM);
            float dis = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);         // Udaljenost do sljedećeg waypointa
            if (dis < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }*/

    }
}
