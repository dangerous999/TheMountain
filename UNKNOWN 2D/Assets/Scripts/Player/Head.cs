using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	void FixedUpdate () {
        //gledanje u mis
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dis = (mPos - (Vector2)transform.position).normalized;
        transform.up = dis;
    }
}
