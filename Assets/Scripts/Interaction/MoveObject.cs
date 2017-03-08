using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : Interactable {

    public float interp = .0005f;
    public bool constantMotion = true;
    public bool startMoving = false;

    public Vector3 locationA;
    public Vector3 locationB;
    bool aStart = true;

    void Awake()
    {
        if(startMoving)
        {
            StartCoroutine("Interact");
        }
    }

    public override IEnumerator Interact()
    {
        Vector3 dest = aStart ? locationB : locationA;
        Vector3 start = aStart ? locationA : locationB;
        while (Vector3.Distance(transform.position, dest) > .5)
        {
            transform.position -= start - Vector3.Lerp(start, dest, interp);
            yield return null;
        }

        aStart = !aStart;
        if(constantMotion)
        {
            StartCoroutine("Interact");
        }
        yield return null;
    }
}
