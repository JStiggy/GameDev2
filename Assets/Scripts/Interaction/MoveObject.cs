using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : Interactable
{

    public float speed = 2f;
    public bool constantMotion = true;
    public bool startMoving = false;

    public Vector3 locationA;
    public Vector3 locationB;
    bool aStart = true;

    void Awake()
    {
        if (startMoving)
        {
            StartCoroutine("Interact");
        }
    }

    public override IEnumerator Interact()
    {
        Vector3 dest = aStart ? locationB : locationA;
        Vector3 start = aStart ? locationA : locationB;
        float currentDist;
        do
        {
            currentDist = Vector3.Distance(transform.position, dest);
            transform.Translate((dest - start) * speed * Time.deltaTime);
            yield return null;
        } while (Vector2.Distance(transform.position, dest) <= currentDist);
        aStart = !aStart;
        if (constantMotion)
        {
            StartCoroutine("Interact");
        }
        yield return null;
    }
}
