using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetizeObject : Interactable
{

    public override IEnumerator Interact()
    {
        if(gameObject.layer == 0)
        {
            gameObject.layer = 9;
        }
        else
        {
            gameObject.layer = 0;
        }
        return null;
    }
}
