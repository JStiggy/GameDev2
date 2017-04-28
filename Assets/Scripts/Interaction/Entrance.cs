using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : Interactable {

    bool playerFade = true;
    GameObject player = null;
    public string nextScene;
    public float nextXPos = 0;
    public float nextYPos = 0;

    public override IEnumerator Interact()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().control = false;
        FadePlayer();
        yield return new WaitForSeconds(1f);
        ChangeScene();
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
        GameManager.Manager.xPos = this.nextXPos;
        GameManager.Manager.yPos = this.nextYPos;
        GameManager.Manager.FadeOut(nextScene);
    }
}
