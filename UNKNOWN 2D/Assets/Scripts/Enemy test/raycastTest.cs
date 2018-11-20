using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastTest : MonoBehaviour {

    public GameObject player;
    public float rayDistance = 2f;                  //duljina raya
    public LayerMask hitLayers;                     //layeri koji mogu biti pogođeni
    public float angle = 30f;
    //public RaycastHit2D hit;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //smijer od neprijatelja do playera
        Vector2 raycastDir = (player.transform.position - transform.position).normalized;
        
        //povlaci liniju u smijeru  (od,do,duljina,layer)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDir, rayDistance, hitLayers);

        //pokazuje liniju jer je inace nevidljiva
        Debug.DrawRay(transform.position, raycastDir*rayDistance);
        if (hit.collider !=null)
        {
            
            Debug.Log(hit.collider.gameObject.tag);
        }

        //Debug.Log(hit.collider.gameObject.name);
        float angle = Vector2.Angle(raycastDir, transform.up);
        Debug.Log(angle);

    }


}
