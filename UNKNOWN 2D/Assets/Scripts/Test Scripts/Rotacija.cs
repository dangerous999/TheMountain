using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacija : MonoBehaviour {

    public GameObject uno;
    public float rotationSpeed = 0.2f;

    private void FixedUpdate()
    {
        Vector3 dir = (uno.transform.position - transform.position).normalized;


        float angle2 = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
        float angle = Vector3.Angle(uno.transform.localPosition, transform.up);
        //float rad = angle * Mathf.Deg2Rad;
        if (angle <= angle2 - 5 || angle >= angle2 + 5)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        Debug.Log(angle2);
        Debug.Log("                     " + angle);
        
            //transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        

        //Debug.Log("Radiani = " + rad);

        //transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        //Debug.Log("Uno = " + dir);
        
        //dir.y = 0;
        //Quaternion rotacija = Quaternion.AngleAxis(angle,Vector3.forward);             //kreira rotaciju
        
        //Debug.Log("Rotacija " + rotacija);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotacija, rotationSpeed * Time.deltaTime);            //rotira do tow waypointa
        
    }
}
