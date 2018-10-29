using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class Enemypathfinding : MonoBehaviour {

    public Transform target;
    private Seeker seeker;
    public float UpdateTime;
    private Rigidbody2D rd;

    public Path path;
    public float speed=10f;
    public ForceMode2D FM;
    public bool PathEnded, see=false;
    public float NextWaypoint = 3f;
    private int CWaypoint=0;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rd = GetComponent<Rigidbody2D>();

        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }
    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            Debug.Log("nema targeta");
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / UpdateTime);

        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            CWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (see)
        {
            if (path == null)
                return;
            if (CWaypoint >= path.vectorPath.Count)
            {
                if (PathEnded)
                    return;
                Debug.Log("end of path");
                PathEnded = true;
                return;

            }
            PathEnded = false;
            transform.up = (target.transform.position - transform.position).normalized;
            Vector3 dir = (path.vectorPath[CWaypoint] - transform.position).normalized;
            dir *= speed * Time.fixedDeltaTime;
            rd.AddForce(dir, FM);
            float dis = Vector3.Distance(transform.position, path.vectorPath[CWaypoint]);
            if (dis < NextWaypoint)
            {
                CWaypoint++;
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
            StartCoroutine(UpdatePath());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        see = false;
    }
}
