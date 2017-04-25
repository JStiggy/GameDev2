using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMagnetization : Interactable
{

    public override IEnumerator Interact()
    {
        this.GetComponent<Magnetizable>().magnetized = !this.GetComponent<Magnetizable>().magnetized;
        return null;
    }
}
