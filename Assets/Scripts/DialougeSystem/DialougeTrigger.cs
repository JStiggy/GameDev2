using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour {

    private DialougeSystem sys;

	// Use this for initialization
	void Start () {
        sys = GameObject.Find("Dialouge System").GetComponent<DialougeSystem>();
        sys.StartCoroutine("PrintDialouge", 0);
	}
}
