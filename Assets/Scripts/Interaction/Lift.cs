using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MoveObject {

    public int SaveFlag = 63;

    // Use this for initialization
    void Start()
    {
        if ((((long)1 << SaveFlag) & GameManager.Manager.playerData.saveFlags) > 0)
        {
            aStart = !aStart;
            this.transform.position = locationB;
        }
    }
}
