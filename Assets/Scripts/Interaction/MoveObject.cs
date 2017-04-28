using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : Interactable
{

    public float speed = 2f;
    public float delay = 0f;
    public bool constantMotion = true;
    public bool startMoving = false;

    public bool updateX = true;

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
        if(!updateX)
        {
            locationA.x = transform.position.x;
            locationB.x = transform.position.x;
        }

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
            yield return new WaitForSeconds(delay);
            StartCoroutine("Interact");
        }
        yield return null;
    }
}
