using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour {

    private DialougeSystem sys;

	// Use this for initialization
	void Start ()
    {
        if ((((long)1 << 32) & GameManager.Manager.playerData.saveFlags) > 0)
        {
            Destroy(gameObject);
        }
        else
        {
            sys = GameObject.Find("Dialouge System").GetComponent<DialougeSystem>();
            sys.StartCoroutine("PrintDialouge", 0);
        }
	}
}
