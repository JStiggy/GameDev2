using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    public float interp = .1f;

    Vector3 locationA;
    Vector3 locationB;
    bool aStart = true;

    void Awake()
    {
        StartCoroutine("MoveLocation");
    }

    public IEnumerator MoveLocation()
    {
        locationA = transform.GetChild(0).position;
        locationB = transform.GetChild(1).position;

        Vector3 dest = aStart ? locationB : locationA;

        while (Vector3.Distance(transform.position, dest) > .5)
        {
            transform.position = Vector3.Lerp(transform.position, dest, interp);
            yield return null;
        }

        aStart = !aStart;
        yield return null;
    }
}
