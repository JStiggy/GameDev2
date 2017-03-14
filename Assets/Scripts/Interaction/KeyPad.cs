using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : Interactable {

    public string answer;

    GameObject player = null;
    Image img = null;

    public Interactable[] interact;

    public Image[] buttonPresses = null;
    public Sprite[] animations = null; 

    void Awake()
    {
        StartCoroutine("Interact");
    }

    int mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    public override IEnumerator Interact()
    {
        img = this.GetComponent<Image>();
        img.enabled = true;

        int location = 0;
        List<string> input = new List<string>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<PlayerController>().control = false;

        while (!Input.GetKeyDown(KeyCode.Q))
        {
            buttonPresses[location].enabled = false;

            img.sprite = animations[input.Count];
            if(Input.GetKeyDown(KeyCode.S))
            {
                location = mod(location + 3, 9);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                location = mod(location - 3, 9);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                location = mod(location - 1, 9);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {

                location = mod(location + 1, 9);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                input.Add((location+1).ToString());
            }
            if(input.Count >= 4)
            {
                img.sprite = animations[4];
                yield return new WaitForSeconds(2);
                print(string.Join("",input.ToArray()).Equals(answer));
                if(string.Join("", input.ToArray()) == answer)
                {

                    img.sprite = animations[5];
                    yield return new WaitForSeconds(2);
                    foreach (Interactable i in interact)
                    {
                        i.StartCoroutine("Interact");
                    }
                    break;
                }
                else
                {
                    img.sprite = animations[6];
                    yield return new WaitForSeconds(2);
                    input = new List<string>();
                }

            }
            buttonPresses[location].enabled = true;
            yield return null;
        }
        buttonPresses[location].enabled = false;
        player.GetComponent<PlayerController>().control = true;
        img.enabled = false;

        yield return null;
    }

}
