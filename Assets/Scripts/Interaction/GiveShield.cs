﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveShield : Interactable
{
    void Start()
    {
        if ((((long)1 << 36) & GameManager.Manager.playerData.saveFlags) > 0)
        {
            Destroy(gameObject);
        }
    }

    public override IEnumerator Interact()
    {
        DialougeSystem sys = GameObject.Find("Dialouge System").GetComponent<DialougeSystem>();
        sys.StartCoroutine("PrintDialouge", 10);

        GameManager.FindObjectOfType<PlayerController>().AbilityCooldown.Glide = true;

        GameManager.Manager.playerData.saveFlags = GameManager.Manager.playerData.saveFlags | ((long)1 << 36);
        GameManager.Manager.playerData.Save(); //This would save it but need to test everything

        Destroy(this.gameObject);
        return null;
    }
}
