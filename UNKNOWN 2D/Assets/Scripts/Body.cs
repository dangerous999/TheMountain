using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

    private GameObject head;
    public float rotationSpeed = 5f;

    private void Start()
    {
        head = GameObject.Find("Head");       
    }

    void Update()
    {       

        float angle = Quaternion.Angle(transform.rotation, head.transform.rotation);
        //if ( Input.GetKey(KeyCode.Mouse0) )
        //{
        //    transform.rotation = head.transform.rotation;

        //}
        //else 
        if (angle > 60f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, head.transform.rotation, rotationSpeed * Time.deltaTime);
        }

    }
    void FixedUpdate()
    {

    }

}
