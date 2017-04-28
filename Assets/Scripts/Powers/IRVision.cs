using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRVision : MonoBehaviour {

    public Camera cam;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P) && GameManager.Manager.playerReference.GetComponent<PlayerController>().AbilityCooldown.Vision)
        {
            cam.enabled = !cam.enabled;
        }
	}
}
