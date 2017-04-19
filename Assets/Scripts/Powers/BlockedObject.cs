using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Put this script on any object that can be blocked by the shield

public class BlockedObject : MonoBehaviour {
    public bool DestroyUponBlock;
    public bool BreakShield;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<Shield> ()) {
			if(DestroyUponBlock)
				Destroy (gameObject);
			if (BreakShield)
				col.gameObject.SetActive (false);
		}
	}
}
