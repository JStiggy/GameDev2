using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour {

    LineRenderer lr;
    public Vector2 direction;

    void Awake()
    {
        lr = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
       RaycastHit2D value = Physics2D.Raycast(this.transform.position, direction, 100f);
       lr.SetPosition(1, direction * value.distance);
       if(value.transform.tag == "Player")
       {
            value.transform.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.Manager.ReloadGame();
            GameManager.Manager.playerReference.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }


}
