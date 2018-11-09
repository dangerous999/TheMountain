using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    public Vector3 offset;
    public GameObject player;
    public float moveAmount = 3f;
    public float smoothTimeY = 0.5f;
    public float smoothTimeX = 0.5f;
    private float horizontal, vertical;
    private Vector2 velocity;
    public bool camWay1 = true;
    float posX, posY;
    // Use this for initialization
    void Start () {
        //player = GameObject.Find("Armor");
        //transform.position = new Vector3(0, 0, -1);
        offset = new Vector3(0, 0, -1);
	}
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");    
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + horizontal * moveAmount, ref velocity.x, smoothTimeX);
        //float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + vertical * moveAmount, ref velocity.y, smoothTimeY);
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 mPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y).;

        Vector2 direction = ((mPos - (Vector2)transform.position) * 0.5f);
        //Debug.Log("direction       " + direction);
        //Debug.Log("Mouse position       " + mPos);
        if (camWay1)
        {
             posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + direction.x, ref velocity.x, smoothTimeX);
             posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + direction.y, ref velocity.y, smoothTimeY);
        }
        else
        {
             posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + direction.x + moveAmount * horizontal, ref velocity.x, smoothTimeX);
             posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + direction.y + moveAmount * vertical, ref velocity.y, smoothTimeY);
        }

        transform.position = new Vector3(posX, posY, -1);

    }
}
