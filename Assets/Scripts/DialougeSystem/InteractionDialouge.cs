using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDialouge : Interactable {

    public bool OneTime = true;
    public int SaveFlag = 63;
    public int PrereqFlag = 63;
    public int dialogue = 3;

    // Use this for initialization
    void Start()
    {
        //If dialouge seen delete trigger (Assuming non repeatable)
        if ((((long)1 << SaveFlag) & GameManager.Manager.playerData.saveFlags) > 0)
        {
            Destroy(this);
        }
        //If prereq not met remove trigger
        if ((((long)1 << PrereqFlag) & GameManager.Manager.playerData.saveFlags) == 0)
        {
            Destroy(this);
        }
    }

    public override IEnumerator Interact()
    {
        GameObject.Find("Dialouge System").GetComponent<DialougeSystem>().StartCoroutine("PrintDialouge", dialogue);
        if (OneTime == true)
        {
            //Destroy(gameObject);
            GameManager.Manager.playerData.saveFlags = GameManager.Manager.playerData.saveFlags | ((long)1 << SaveFlag);
            GameManager.Manager.playerData.Save(); //This would save it but need to test everything
        }
        return null;
    }
}
