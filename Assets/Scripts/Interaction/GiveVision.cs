using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveVision : Interactable
{

    public Interactable door;

    void Start()
    {
        if ((((long)1 << 33) & GameManager.Manager.playerData.saveFlags) > 0)
        {
            Destroy(gameObject);
        }
    }

    public override IEnumerator Interact()
    {
        DialougeSystem sys = GameObject.Find("Dialouge System").GetComponent<DialougeSystem>();
        sys.StartCoroutine("PrintDialouge", 0);

        GameManager.FindObjectOfType<PlayerController>().AbilityCooldown.Vision = true;

        door.StartCoroutine("Interact");
        Destroy(this.gameObject);
        GameManager.Manager.playerData.saveFlags = GameManager.Manager.playerData.saveFlags | ((long)1 << 33);
        GameManager.Manager.playerData.Save(); //This would save it but need to test everything
        return null;
    }
}
