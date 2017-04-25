using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMagnetization : Interactable {

    public override IEnumerator Interact()
    {
        //Would enable Magnetization here
        Destroy(this.gameObject);
        return null;
    }

}
