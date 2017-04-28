using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMagnetization : Interactable {

    public override IEnumerator Interact()
    {
        DialougeSystem sys = GameObject.Find("Dialouge System").GetComponent<DialougeSystem>();
        sys.StartCoroutine("PrintDialouge", 7);
        Destroy(this.gameObject);
        return null;
    }

}
