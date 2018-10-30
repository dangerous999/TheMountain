using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    GameObject head;
    public float speed=50f;
    private void Start()
    {
        head = GameObject.Find("Head");
        
    }

    void FixedUpdate()
    {
        //radi sordoff
        /*
        Vector2 HGangle = new Vector2(HG.transform.position.x, HG.transform.position.y);
        Vector2 AGangle = new Vector2(AG.transform.position.x, AG.transform.position.y);
        float angle = Vector2.Angle(HGangle, AGangle);
        Debug.Log(angle);
        if (angle > 60)
        {
            Vector2 aa = new Vector2(HG.transform.position.x+AG.transform.position.x/3, HG.transform.position.y+AG.transform.position.y/2);
            transform.up = aa;
        }
        */

        //2.0
        /*
        Quaternion Head = Quaternion.identity(transform.rotation);

        float angle = Quaternion.Angle(HG.transform.rotation,AG.transform.rotation);
        Debug.Log(angle);
        if (angle > 60f)
        {
            
            transform.rotation = Quaternion.Slerp(AG.transform.rotation,transform.rotation,speed);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.rotation = Quaternion.Slerp(AG.transform.rotation, HG.transform.rotation, speed);
        }
        */
        //3.0

        float angle = Quaternion.Angle(this.gameObject.transform.rotation, head.transform.rotation);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.rotation = head.transform.rotation;

        }
        else if (angle > 60)
        {
            transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation , head.transform.rotation, speed);
        }
    }
}
/*
GameObject head, armor;
public float speed = 50f;
private void Start()
{
    head = GameObject.Find("Head");
    armor = GameObject.Find("Armor");
}

void FixedUpdate()
{
    //radi sordoff
    /*
    Vector2 HGangle = new Vector2(HG.transform.position.x, HG.transform.position.y);
    Vector2 AGangle = new Vector2(AG.transform.position.x, AG.transform.position.y);
    float angle = Vector2.Angle(HGangle, AGangle);
    Debug.Log(angle);
    if (angle > 60)
    {
        Vector2 aa = new Vector2(HG.transform.position.x+AG.transform.position.x/3, HG.transform.position.y+AG.transform.position.y/2);
        transform.up = aa;
    }
    */

    //2.0
    /*
    Quaternion Head = Quaternion.identity(transform.rotation);

    float angle = Quaternion.Angle(HG.transform.rotation,AG.transform.rotation);
    Debug.Log(angle);
    if (angle > 60f)
    {

        transform.rotation = Quaternion.Slerp(AG.transform.rotation,transform.rotation,speed);
    }
    if (Input.GetKey(KeyCode.Mouse0))
    {
        transform.rotation = Quaternion.Slerp(AG.transform.rotation, HG.transform.rotation, speed);
    }
    
    //3.0

    float angle = Quaternion.Angle(armor.transform.rotation, head.transform.rotation);
    if (Input.GetKey(KeyCode.Mouse0))
    {
        transform.rotation = head.transform.rotation;

    }
    else if (angle > 60)
    {
        transform.rotation = Quaternion.Slerp(armor.transform.rotation, head.transform.rotation, speed);
    }
}
}*/

