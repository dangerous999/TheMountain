using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {
    Vector3 offset;
   public GameObject player;

	// Use this for initialization
	void Start () {
        //player = GameObject.Find("Armor");
        offset = new Vector3(0, 0, -1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.position = (player.transform.position + offset);
	}
}
