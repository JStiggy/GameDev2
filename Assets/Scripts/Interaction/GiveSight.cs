using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveSight : Interactable
{

    public override IEnumerator Interact()
    {
        print("D");
        GameObject.FindGameObjectWithTag("Player").GetComponent<CreateShadows>().distance = 6;
        Destroy(this.gameObject);
        return null;
    }
}
