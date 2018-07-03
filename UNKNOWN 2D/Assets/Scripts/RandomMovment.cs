using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovment : MonoBehaviour {

    private Vector2 Movment;
    public float moveSpeed;

    void RanMovment()
    {
        Movment = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        transform.position = Vector2.MoveTowards(transform.position, Movment, moveSpeed * Time.deltaTime);
    }
}
