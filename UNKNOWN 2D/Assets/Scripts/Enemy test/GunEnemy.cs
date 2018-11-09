using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour {

    public Transform target;
    public GameObject bullet;
    public float speed = 5f;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Fire()
    {
        Vector2 direction = (target.transform.position- transform.position).normalized;
        // Creates new bullet at transform.position
        GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation);
        // Shoots bullet in direction of mouse
        bulletClone.GetComponent<Rigidbody2D>().velocity = bulletClone.transform.up*speed;


    }
}
