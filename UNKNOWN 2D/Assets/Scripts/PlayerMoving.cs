using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour {
    
    public float FORCE = 200f;
    public float SprintSpeed = 1.25f;
    public float SneakSpeed = 0.25f;
    public Rigidbody2D rb2d;

    GameObject enemy;
    //CircleCollider2D Detection;
    // Update is called once per frame
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemy = GameObject.Find("Enemy");
        //Detection = enemy.GetComponent<CircleCollider2D>();
        
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) //ljevo
        {

            
            if (Input.GetKey(KeyCode.LeftShift))
            {
//                transform.Translate(-Speed * Time.deltaTime * SprintSpeed, 0, 0);
                rb2d.AddForce(new Vector2(-FORCE * Time.deltaTime * SprintSpeed, 0));
                //Detection.enabled = true;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                //Detection.enabled = false;
//                transform.Translate(-Speed * Time.deltaTime * SneakSpeed, 0, 0);
                rb2d.AddForce(new Vector2(-FORCE * Time.deltaTime * SneakSpeed, 0));
            }
            else {
                //Detection.enabled = true;
                rb2d.AddForce(new Vector2(-FORCE * Time.deltaTime, 0));
                //transform.Translate(-Speed * Time.deltaTime, 0, 0);
            }

        }
        if (Input.GetKey(KeyCode.D))//desno
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //transform.Translate(Speed * Time.deltaTime * SprintSpeed, 0, 0);
                rb2d.AddForce(new Vector2(FORCE * Time.deltaTime * SprintSpeed, 0));
                //Detection.enabled = true;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                //Detection.enabled = false;
                //transform.Translate(Speed * Time.deltaTime * SneakSpeed, 0, 0);
                rb2d.AddForce(new Vector2(FORCE * Time.deltaTime * SneakSpeed, 0));
            }
            else
            {
                //Detection.enabled = true;
                //transform.Translate(Speed * Time.deltaTime, 0, 0);
                rb2d.AddForce(new Vector2(FORCE * Time.deltaTime, 0));
            }

        }
        if (Input.GetKey(KeyCode.W))//gore
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //transform.Translate(0, Speed * Time.deltaTime * SprintSpeed, 0);
                rb2d.AddForce(new Vector2(0, FORCE * Time.deltaTime * SprintSpeed));
                //Detection.enabled = true;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                //Detection.enabled = false;
                //transform.Translate(0, Speed * Time.deltaTime * SneakSpeed, 0);
                rb2d.AddForce(new Vector2(0, FORCE * Time.deltaTime * SneakSpeed));
            }
            else 
            {
                //Detection.enabled = true;
                //transform.Translate(0, Speed * Time.deltaTime, 0);
                rb2d.AddForce(new Vector2(0, FORCE * Time.deltaTime ));
            }
        }
        if (Input.GetKey(KeyCode.S))//dolje
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //transform.Translate(0, -Speed * Time.deltaTime * SprintSpeed, 0);
                rb2d.AddForce(new Vector2(0,-FORCE * Time.deltaTime * SprintSpeed));
                //Detection.enabled = true;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                //Detection.enabled = false;
                //transform.Translate(0, -Speed * Time.deltaTime * SneakSpeed, 0);
                rb2d.AddForce(new Vector2(0,-FORCE * Time.deltaTime * SneakSpeed));
            }
            else
            {
                //Detection.enabled = true;
                //transform.Translate(0, -Speed * Time.deltaTime, 0);
                rb2d.AddForce(new Vector2(0,-FORCE * Time.deltaTime ));
            }
        }
        

    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT");
    }*/
}
