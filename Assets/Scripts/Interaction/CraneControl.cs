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
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            }
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.20f, 2.85f), Mathf.Clamp(transform.position.y, 1.21f, 8.05f), 0);
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
