using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Interactable {

    Animator anim = null;
    bool playerFade = true;
    GameObject player = null;
    public string nextScene;
    public float nextXPos = 0;
    public float nextYPos = 0;

    public override IEnumerator Interact()
    {
        print("Elev");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (anim == null)
            anim = this.GetComponent<Animator>();
        player.GetComponent<PlayerController>().control = false;
        anim.SetTrigger("Activated");
        yield return null;
    }

    void FadePlayer()
    {
        GameManager.Manager.SpriteFade(!playerFade, player);
        playerFade = !playerFade;
        if (playerFade == true)
        {
            this.enabled = false;
        }
    }

    void ChangeScene()
    {
        GameManager.Manager.FadeOut(nextScene);
    }
}
