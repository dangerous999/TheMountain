using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

    private GameObject head;
    private GameObject arms;
    public float rotationSpeed = 5f;
    public float rotationSpeedForMouseClick = 30f;
    public bool angleReached = false;
    private float angle;

    private void Start()
    {
        head = GameObject.Find("Head");
        arms = GameObject.Find("Arms");
        
    }

    void Update()
    {
        if(!angleReached)
        {
            angle = Quaternion.Angle(transform.rotation, head.transform.rotation);
        }
        
        if ( Input.GetKey(KeyCode.Mouse0) )
        {

            if (angle>10f && !angleReached)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, head.transform.rotation, rotationSpeedForMouseClick * Time.deltaTime);
            }
            else
            {
                angleReached = true;
                
                transform.rotation = head.transform.rotation;
            }
        }else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            angleReached = false;
        }
        else 
        if (angle > 60f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, head.transform.rotation, rotationSpeed * Time.deltaTime);
        }
        //Debug.Log(transform.rotation.z - head.transform.rotation.z);
        

    }
    void FixedUpdate()
    {

    }

}
