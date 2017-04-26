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
    [HideInInspector]
    public bool aStart = true;

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
        currentDist = Vector3.Distance(transform.position, dest);
        transform.Translate((dest - start).normalized * speed * Time.deltaTime);
        yield return null;
        while (Vector2.Distance(transform.position, dest) <= currentDist)
        {
            currentDist = Vector3.Distance(transform.position, dest);
            transform.Translate((dest - start).normalized * speed * Time.deltaTime);
            if(!(Vector2.Distance(transform.position, dest) <= currentDist))
            {
                break;
            }
            yield return null;
        }
        aStart = !aStart;
        transform.position = dest;
        if (constantMotion)
        {
            StartCoroutine("Interact");
        }
        yield return null;
    }
}
