using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour {

    public Interactable[] interactableObjects;
    public GameObject interactionIcon;

    private GameObject interactButton;

    void Awake()
    {
        if (interactButton == null)
        {
            interactButton = Instantiate(interactionIcon, new Vector3(transform.position.x, transform.position.y + GetComponent<SpriteRenderer>().sprite.bounds.max.y * transform.localScale.y +.5f, transform.position.z), Quaternion.identity);
            interactButton.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            interactButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            interactButton.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<PlayerController>().activate)
        {
            foreach (Interactable i in interactableObjects)
            {
                i.StopAllCoroutines();
                i.StartCoroutine("Interact");
                interactButton.SetActive(false);
            }
        }
    }

}
