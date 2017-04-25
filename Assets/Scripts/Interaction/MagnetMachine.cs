using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMachine : Interactable
{
    public ParticleSystem smokeSystem;
    public GameObject popoffMagPanel;
    public GameObject magnetChip;
    public Sprite ExplodedTexture;

    void Start()
    {
        if(((1 << 1) & GameManager.Manager.playerData.saveFlags) > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = ExplodedTexture;
        }
    }

    public override IEnumerator Interact()
    {
        Instantiate(smokeSystem,this.transform);
        yield return new WaitForSeconds(1);
        Instantiate(popoffMagPanel, new Vector3(transform.position.x, transform.position.y, -2f), Quaternion.identity);
        gameObject.GetComponent<SpriteRenderer>().sprite = ExplodedTexture;
        Instantiate(magnetChip, new Vector3(transform.position.x, transform.position.y, -2f), Quaternion.identity);
        yield return null;
    }
}
