using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTrigger : MonoBehaviour {

    public bool OneTime = true;
    public bool dest = true;
    public int SaveFlag = 63;
    public Interactable[] interactableObjects;

    // Use this for initialization
    void Start () {
		if( (((long)1 << SaveFlag) & GameManager.Manager.playerData.saveFlags) > 0 && dest)
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
        if(OneTime == true)
        {
            Destroy(gameObject);
        }
        GameManager.Manager.playerData.saveFlags = GameManager.Manager.playerData.saveFlags | ((long)1 << SaveFlag);
        GameManager.Manager.playerData.Save(); //This would save it but need to test everything
    }
}
