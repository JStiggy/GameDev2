using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour {

    public Interactable[] interactableObjects;

    void OnTriggerStay2D(Collider2D collider)
    {
       
        if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<PlayerController>().activate)
        {
            foreach (Interactable i in interactableObjects)
            {
                print(collider.name);
                i.StopAllCoroutines();
                i.StartCoroutine("Interact");
            }
        }
    }

}
