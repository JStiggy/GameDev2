using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MoveObject
{

    public int SaveFlag = 63;

    // Use this for initialization
    void Start()
    {
        if (GameManager.Manager.yPos > 10f)
        {
            aStart = !aStart;
            this.transform.position = locationB;
        }
    }
}
