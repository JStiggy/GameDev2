using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour {

    public Interactable[] interactableObjects;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown("e") && collider.gameObject.tag == "Player")
        {
            foreach (Interactable i in interactableObjects)
            {
                i.StopAllCoroutines();
                i.StartCoroutine("Interact");
            }
        }
    }

}
