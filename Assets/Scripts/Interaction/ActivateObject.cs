using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour {

	// Use this for initialization
	void Update()
    {
        if((((long)1 << 3) & GameManager.Manager.playerData.saveFlags) > 0)
        {
            GetComponent<Activatable>().enabled = true;
            Destroy(this);
        }
    }
}
