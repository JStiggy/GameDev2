using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioSystem : MonoBehaviour
{
    private AudioSource aS;
    public AudioClip noiseStart;
    public AudioClip noiseLoop;
    void Start()
    {
        aS = GetComponent<AudioSource>();
        StartCoroutine("playSound");
    }

    IEnumerator playSound()
    {
        aS.clip = noiseStart;
        aS.maxDistance = 1000f;
        aS.Play();
        yield return new WaitForSeconds(aS.clip.length);
        aS.clip = noiseLoop;
        aS.loop = true;
        aS.Play();
    }
}