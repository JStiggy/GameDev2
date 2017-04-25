using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTrigger : MonoBehaviour {

    public int SaveFlag = 63;
    public Interactable[] interactableObjects;

    // Use this for initialization
    void Start () {
		if( ((1 << SaveFlag) & GameManager.Manager.playerData.saveFlags) > 0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            foreach (Interactable i in interactableObjects)
            {
                i.StopAllCoroutines();
                i.StartCoroutine("Interact");
            }
        }
    }
}
