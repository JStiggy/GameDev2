using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveSight : Interactable
{

    void Start()
    {
        if ((((long)1 << 32) & GameManager.Manager.playerData.saveFlags) > 0)
        {
            Destroy(gameObject);
        }
    }

    public override IEnumerator Interact()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CreateShadows>().distance = 5;
        Destroy(this.gameObject);
        GameManager.Manager.playerData.saveFlags = GameManager.Manager.playerData.saveFlags | ((long)1 << 32);
        GameManager.Manager.playerData.Save(); //This would save it but need to test everything
        return null;
    }
}
