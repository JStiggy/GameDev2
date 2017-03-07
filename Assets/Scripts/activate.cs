using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate : MonoBehaviour {

    // Use this for initialization
    public bool ac = false;
    public GameObject ob;//gameobject
    public Component compofob;//thecomp of the game ob you want to change
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (compofob.GetType() == typeof(GameManager)) //example
        //{
            if (ac)
            {
                //example ob.GetComponent<GameManager>().enabled = false;
            }
            else
            {
                //example ob.GetComponent<GameManager>().enabled = true;
            }
        //}
        //copy this for all possible component name
		
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown("e")){
            print("press e");
            ac = !ac;
        }

    }
}
