﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveSight : Interactable
{

    public override IEnumerator Interact()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CreateShadows>().distance = 5;
        Destroy(this.gameObject);
        return null;
    }
}
