using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneControl : Interactable
{
    public GameObject magnet;

    public override IEnumerator Interact()
    {
        GameManager.Manager.playerReference.GetComponent<PlayerController>().control = false;
        while (!Input.GetKeyDown(KeyCode.Q))
        {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.down * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach(Magnetizable m in magnet.GetComponentsInChildren<Magnetizable>())
                {
                    m.gameObject.transform.parent = null;
                }

                if (magnet.layer == 0)
                {
                    magnet.layer = 9;
                }
                else
                {
                    magnet.layer = 0;
                }
            }
            
            yield return null;
        }
        GameManager.Manager.playerReference.GetComponent<PlayerController>().control = true;

        yield return null;
        
    }
}
