using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RechargeStation : Interactable {

    Animator anim = null;
    bool playerFade = true;
    GameObject player = null;

    public override IEnumerator Interact()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (anim == null)
            anim = this.GetComponent<Animator>();
        
        GameManager.Manager.playerData.energyReserve = 100f;
        GameManager.Manager.playerData.saveScene = SceneManager.GetActiveScene().name;
        GameManager.Manager.playerData.saveXPosition = player.transform.position.x;
        GameManager.Manager.playerData.saveYPosition = player.transform.position.y;
        GameManager.Manager.playerData.Save();

        anim.SetBool("Charging", true);
        yield return null;
    }

    void FadePlayer()
    {
        GameManager.Manager.SpriteFade(!playerFade, player);
        anim.SetBool("Charging", playerFade);
        playerFade = !playerFade;
        if (playerFade == true)
            this.enabled = false;
    }
}
