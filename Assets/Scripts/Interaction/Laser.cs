using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    LineRenderer lr;

    void Awake()
    {
        lr = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
       RaycastHit2D value = Physics2D.Raycast(this.transform.position, Vector2.right, 100f);
       lr.SetPosition(1, Vector3.right * value.distance);
        if(value.transform.tag == "Player")
        { 
            value.transform.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.Manager.ReloadGame();
        }
    }


}
