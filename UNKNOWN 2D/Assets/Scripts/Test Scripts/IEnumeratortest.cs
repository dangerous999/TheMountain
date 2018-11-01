using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnumeratortest : MonoBehaviour {

    public float wait = 2f;

	IEnumerator Jedan()
    {
        Debug.Log("wait 1");
        yield return new WaitForSeconds(wait);
        StartCoroutine(Dva());
    }
    IEnumerator Dva()
    {
        Debug.Log("wait 2");
        yield return new WaitForSeconds(wait);
        StartCoroutine(Dva());
    }
    private void Start()
    {
        StartCoroutine(Jedan());
    }
}
