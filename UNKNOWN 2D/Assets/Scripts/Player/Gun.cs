using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform target;
    public GameObject bullet;
    public float timer = 0f, shootInterval = 0.5f, shootTimer = 0;
    public bool shoot = false;
    public float holdModifier = 100f;
    public float holdTime = 1f;
    private float animSpeed;


    //bananino dodavanje
    private Animator anim;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        animSpeed = anim.speed;
    }

    // Update is called once per frame

    /*dinče
    void Update()
    {

        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (timer > shootInterval)
            {
                Fire();
                timer = 0f;
            }

        }

    }*/


    void Update()
    {
        /*if (Input.GetKey(KeyCode.Mouse0))
        {
            if (timer < holdTime)
            {
                timer += Time.deltaTime;

                anim.SetBool("chargeFire", true);
            }
            else
            {
                anim.SetBool("fire", true);
            }



        }
        else
        {
            timer = 0;
            anim.SetBool("fire", false);
            anim.SetBool("chargeFire", false);
        }*/
        shootTimer += Time.deltaTime;
        if (shootTimer > shootInterval)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //animacija od 1-do zadnje
                if (timer <= holdTime)
                {
                    timer += Time.deltaTime;
                    //prelazi na sljedecu animaciju
                    anim.SetBool("fire", true);
                }
                else
                {
                    //ostaje na zadnjoj animaciji
                    anim.SetBool("chargeFire", true);
                }


            }
            //ovisno o kojoj animaciji puca određeno daleko kada pusti miš
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Fire();
                anim.SetBool("fire", false);
                anim.SetBool("chargeFire", false);
                timer = 0f;
                shootTimer = 0;
            }

        }

            
            //ovisno o kojoj animaciji puca određeno daleko kada pusti miš


        /*
            timer += Time.deltaTime;
            if (timer < holdTime/2)
            {
                anim.SetBool("stop", true);

            }
            else if(timer < holdTime)
            {
                timer += Time.deltaTime;
                anim.SetBool("chargeFire", true);
            }
        }else if (timer < holdTime / 2)
        {
            anim.SetBool("fire", true);
        }
        else
        {
            anim.SetBool("fire", false);
            anim.SetBool("chargeFire", false);
        }
        */



    }
    void Fire()
    {
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        // Creates new bullet at transform.position
        GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation);
        // Shoots bullet in direction of mouse
        bulletClone.GetComponent<Rigidbody2D>().velocity = direction * (bulletClone.GetComponent<Bullet>().bulletSpeed + timer * holdModifier);
        

    }
    void Pause()
    {
        anim.speed = 0;
    }

}
