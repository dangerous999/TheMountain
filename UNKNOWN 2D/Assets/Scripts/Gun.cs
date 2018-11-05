using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform target;
    public GameObject bullet;
    public float timer = 0f, shootInterval = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (timer > shootInterval)
            {
                // Direction from us to mouse position 
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                // Creates new bullet at transform.position
                GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation);
                // Shoots bullet in direction of mouse
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletClone.GetComponent<Bullet>().bulletSpeed;
                timer = 0f;
            }

        }

    }

}
