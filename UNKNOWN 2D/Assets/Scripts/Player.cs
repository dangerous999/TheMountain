using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float Speed =5f;
    public float SprintSpeed1=1.25f;
    public float SneakSpeed = 0.25f;
    Transform TT;
    GameObject enemy;
    CircleCollider2D Detection;
    // Use this for initialization
    void Start()
    {

        enemy = GameObject.Find("Enemy");
        TT = GetComponent<Transform>();
        Detection = enemy.GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) //ljevo
        {
            transform.Translate(-Speed * Time.deltaTime, 0, 0);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(-Speed * Time.deltaTime * SprintSpeed1, 0, 0);
            }
            if (Input.GetKey(KeyCode.C))
            {
                Debug.Log("TU JE");
                Detection.enabled = false;
                transform.Translate(-Speed * Time.deltaTime * SneakSpeed, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.D))//desno
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Speed * Time.deltaTime * SprintSpeed1, 0, 0);
            }
            if (Input.GetKey(KeyCode.C))
            {
                Detection.enabled = false;
                transform.Translate(Speed * Time.deltaTime * SneakSpeed, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.W))//gore
        {
            transform.Translate(0, Speed * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, Speed * Time.deltaTime * SprintSpeed1, 0);
            }
            if (Input.GetKey(KeyCode.C))
            {
                Detection.enabled = false;
                transform.Translate(Speed * Time.deltaTime * SneakSpeed, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.S))//dolje
        {
            transform.Translate(0, -Speed * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, -Speed * Time.deltaTime * SprintSpeed1, 0);
            }
            if (Input.GetKey(KeyCode.C))
            {
                Detection.enabled = false;
                transform.Translate(-Speed * Time.deltaTime * SneakSpeed, 0, 0);
            }
        }


        //gledanje u mis
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dis = (mPos - (Vector2)transform.position).normalized;
        transform.up = dis;
		
	}
}
