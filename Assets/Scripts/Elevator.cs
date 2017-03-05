using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    Animator anim = null;
    bool playerFade = true;
    GameObject player = null;
    public string nextScene;

    void OnEnable()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (anim == null)
            anim = this.GetComponent<Animator>();

        anim.SetTrigger("Activated");
    }

    void FadePlayer()
    {
        GameManager.Manager.SpriteFade(!playerFade, player);
        playerFade = !playerFade;
        if (playerFade == true)
            this.enabled = false;
    }

    void ChangeScene()
    {
        GameManager.Manager.FadeOut(nextScene);
    }
}
