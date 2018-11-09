using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnHuman : MonoBehaviour {

    GameObject player;
    private PolygonCollider2D Sight;
    public float lookDistance=6f;
    public float attackDistance=4f;
    public float moveSpeed= 5f;
    public float attackRange = 2f;
    private float orginalLookDistance, orginalAttackDistance, orginalAttackRange;
    private bool OutOfRange=true;
    private Vector2 Movment;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        Sight = GetComponent<PolygonCollider2D>();
        orginalAttackDistance = attackDistance;
        orginalAttackRange = attackRange;
        orginalLookDistance = lookDistance;
        
    }
	
	// Update is called once per frame
	void Update () {

       /* if (OutOfRange)
        {
            RanMovment();
        }*/

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (other.CompareTag("Player") && distance < lookDistance)
        {
            lookDistance = 20f;
            //Debug.Log("Triggerd enter");
            transform.up = (player.transform.position - transform.position).normalized;
            OutOfRange = false;
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if(other.CompareTag("Player") && distance < attackRange)//ako je u range za napad napada
        {
            //Debug.Log("attack");
            transform.up = (player.transform.position-transform.position).normalized;
        }
        else if (other.CompareTag("Player") && distance < attackDistance)//ako je dovoljno blizu ide porema njemu
        {
            //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            attackDistance = 10f;
            transform.up = (player.transform.position - transform.position).normalized;

        }
        else if (other.CompareTag("Player") && distance < lookDistance)//samo ga gleda iz daljine
        {
            lookDistance = 20f;
            //Debug.Log("Triggerd stay");
            transform.up = (player.transform.position - transform.position).normalized;

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        attackDistance=orginalAttackDistance;
        attackRange=orginalAttackRange;
        lookDistance=orginalLookDistance;
        OutOfRange = true;
    }

    /*void RanMovment()
    {
        Movment = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        transform.position = Vector2.MoveTowards(transform.position, Movment, moveSpeed/2 * Time.deltaTime);
      
    }*/
}
