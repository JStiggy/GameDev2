using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    MeshRenderer mesh;
    // Use this for initialization
    void Start()
    {
        mesh = this.GetComponent<MeshRenderer>();
        this.StartCoroutine("Flicker");
    }

    IEnumerator Flicker()
    {

        yield return new WaitForSeconds(Random.Range(.25f, 5f));
        mesh.enabled = false;
        yield return new WaitForSeconds(Random.Range(.25f, .45f));
        mesh.enabled = true;
        this.StartCoroutine("Flicker");
        yield return null;
    }
}
