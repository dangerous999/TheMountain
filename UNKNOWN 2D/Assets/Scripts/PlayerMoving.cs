using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour {
    
    public float speed = 200f;
    public float sprintSpeed = 1.25f;
    public float sneakSpeed = 0.25f;
    public float diagonalSpeedModifier = 0.75f;
    private Rigidbody2D rb2d;
    private float horizontal, vertical;

    // Update is called once per frame
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        #region DinoMovement
        if (horizontal != 0 && vertical != 0) // Diagonal movement -> reduce speed
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Sprint speed
            {
                rb2d.AddForce(new Vector2(horizontal * speed * sprintSpeed * diagonalSpeedModifier, vertical * speed * sprintSpeed * diagonalSpeedModifier));
            }
            else if (Input.GetKey(KeyCode.C))  // Sneak speed
            {
                rb2d.AddForce(new Vector2(horizontal * speed * sneakSpeed * diagonalSpeedModifier, vertical * speed * sneakSpeed * diagonalSpeedModifier));
            }
            else                                 // Normal speed
            {
                rb2d.AddForce(new Vector2(horizontal * speed * diagonalSpeedModifier, vertical * speed * diagonalSpeedModifier));
            }
        }
        else 
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Sprint speed
            {
                rb2d.AddForce(new Vector2(horizontal * speed * sprintSpeed, vertical * speed * sprintSpeed));
            }
            else if (Input.GetKey(KeyCode.C))   // Sneak speed
            {
                rb2d.AddForce(new Vector2(horizontal * speed * sneakSpeed, vertical * speed * sneakSpeed));
            }
            else                                // Normal speed
            {
                rb2d.AddForce(new Vector2(horizontal * speed, vertical * speed));
            }
        }
        #endregion

        #region BananaMovement

        /*
        if (Input.GetKey(KeyCode.A)) //ljevo
        {           
            if (Input.GetKey(KeyCode.LeftShift))
            {
//                transform.Translate(-Speed * Time.deltaTime * SprintSpeed, 0, 0);
//                rb2d.AddForce(new Vector2(-speed * Time.deltaTime * sprintSpeed, 0));
                rb2d.velocity = new Vector2(-speed  * sprintSpeed, rb2d.velocity.y);
                //Detection.enabled = true;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                //Detection.enabled = false;
//                transform.Translate(-Speed * Time.deltaTime * SneakSpeed, 0, 0);
//                rb2d.AddForce(new Vector2(-speed * Time.deltaTime * sneakSpeed, 0));
                rb2d.velocity = new Vector2(-speed  * sneakSpeed, rb2d.velocity.y);
            }
            else {
                //Detection.enabled = true;
                //rb2d.AddForce(new Vector2(-speed * Time.deltaTime, 0));
                rb2d.velocity = new Vector2(-speed  * sneakSpeed, rb2d.velocity.y);
                //transform.Translate(-Speed * Time.deltaTime, 0, 0);
            }

        }
        if (Input.GetKey(KeyCode.D))//desno
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //transform.Translate(Speed * Time.deltaTime * SprintSpeed, 0, 0);
                //rb2d.AddForce(new Vector2(speed * Time.deltaTime * sprintSpeed, 0));
                rb2d.velocity = new Vector2(speed  * sprintSpeed, rb2d.velocity.y);
                //Detection.enabled = true;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                //Detection.enabled = false;
                //transform.Translate(Speed * Time.deltaTime * SneakSpeed, 0, 0);
               // rb2d.AddForce(new Vector2(speed * Time.deltaTime * sneakSpeed, 0));
                rb2d.velocity = new Vector2(speed  * sneakSpeed, rb2d.velocity.y);
            }
            else
            {
                //Detection.enabled = true;
                //transform.Translate(Speed * Time.deltaTime, 0, 0);
                //rb2d.AddForce(new Vector2(speed * Time.deltaTime, 0));
                rb2d.velocity = new Vector2(speed  * sneakSpeed, rb2d.velocity.y);
            }

        }
        if (Input.GetKey(KeyCode.W))//gore
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //transform.Translate(0, Speed * Time.deltaTime * SprintSpeed, 0);
                //rb2d.AddForce(new Vector2(0, speed * Time.deltaTime * sprintSpeed));
                rb2d.velocity = new Vector2(rb2d.velocity.x, speed  * sprintSpeed);
                //Detection.enabled = true;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                //Detection.enabled = false;
                //transform.Translate(0, Speed * Time.deltaTime * SneakSpeed, 0);
//                rb2d.AddForce(new Vector2(0, speed * Time.deltaTime * sneakSpeed));
                rb2d.velocity = new Vector2(rb2d.velocity.x, speed  * sneakSpeed);
            }
            else 
            {
                //Detection.enabled = true;
                //transform.Translate(0, Speed * Time.deltaTime, 0);
                //rb2d.AddForce(new Vector2(0, speed * Time.deltaTime ));
                rb2d.velocity = new Vector2(rb2d.velocity.x, speed  * sneakSpeed);
            }
        }
        if (Input.GetKey(KeyCode.S))//dolje
        {           
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //transform.Translate(0, -Speed * Time.deltaTime * SprintSpeed, 0);
//                rb2d.AddForce(new Vector2(0,-speed * Time.deltaTime * sprintSpeed));
                rb2d.velocity = new Vector2(rb2d.velocity.x, -speed  * sprintSpeed);
                //Detection.enabled = true;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                //Detection.enabled = false;
                //transform.Translate(0, -Speed * Time.deltaTime * SneakSpeed, 0);
                //rb2d.AddForce(new Vector2(0,-speed * Time.deltaTime * sneakSpeed));
                rb2d.velocity = new Vector2(rb2d.velocity.x, -speed  * sneakSpeed);
            }
            else
            {
                //Detection.enabled = true;
                //transform.Translate(0, -Speed * Time.deltaTime, 0);
                //rb2d.AddForce(new Vector2(0,-speed * Time.deltaTime ));
                rb2d.velocity = new Vector2(rb2d.velocity.x, -speed );
            }
        }
        */
        #endregion

    }

}
