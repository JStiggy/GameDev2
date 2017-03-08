using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{

    public Interactable[] interactableObjects;
    private Animator anim;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    public virtual void OnTriggerStay2D(Collider2D collider)
    {
        anim.SetBool("Activated", true);
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
       anim.SetBool("Activated", false);
       foreach (Interactable i in interactableObjects)
       {
            i.StopAllCoroutines();
            i.StartCoroutine("Interact");
       }
    }

    public virtual void OnTriggerExit2D(Collider2D collider)
    {
        anim.SetBool("Activated", true);
        foreach (Interactable i in interactableObjects)
        {
            i.StopAllCoroutines();
            i.StartCoroutine("Interact");
        }
    }

}
