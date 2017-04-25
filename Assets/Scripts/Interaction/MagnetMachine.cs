using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMachine : Interactable
{
    public override IEnumerator Interact()
    {
        print("D");
        GameObject.FindGameObjectWithTag("Player").GetComponent<CreateShadows>().distance = 5;
        Destroy(this.gameObject);
        return null;
    }
}
