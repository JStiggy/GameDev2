using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class numpad : MonoBehaviour {

    // this script need to be attacth to a panel object on scene
    public Button[] yourButton;//attatch the existing UI buttons with the name from 1 2 3 etc from the scene, in my case i make a bunch of button from 0->9 on the scen with approprited name 0 1 2 etc
    //you need to make those manually then add them to the array
    public string current = "";
    public string num="1234";//type in the password you want it to have

    public Interactable interactableObject;

    // Update is called once per frame
    void Update()
    {
        print(current);
        if (current == num)
        {
            interactableObject.StartCoroutine("Interact");
            current = "";
            return;
        }
        

            
    }

    void dosomething()
    {
        //your code here
    }
    //uncomment if there is a trigger
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player") return;

        if (Input.GetKeyDown("e"))
        {
            for (int i = 0; i < yourButton.Length; i++)
            {
                Button btn = yourButton[i].GetComponent<Button>();
                Image img = yourButton[i].GetComponent<Image>();
                Text text = yourButton[i].GetComponentInChildren<Text>();
                


                yourButton[i].enabled = !(yourButton[i].enabled);
                img.enabled = !(img.enabled);
                text.enabled = !(text.enabled);


            }
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player") return;
        current = "";
        for (int i = 0; i < yourButton.Length; i++)
        {
            Button btn = yourButton[i].GetComponent<Button>();
            Image img = yourButton[i].GetComponent<Image>();
            Text text = yourButton[i].GetComponentInChildren<Text>();



            yourButton[i].enabled = !(yourButton[i].enabled);
            img.enabled = !(img.enabled);
            text.enabled = !(text.enabled);


        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        current = "";
    }
    void Start()
    {
        current = "";
        for (int i = 0; i < yourButton.Length; i++)
        {
            Button btn = yourButton[i].GetComponent<Button>();
            btn.onClick.AddListener(() => TaskOnClick(btn.name));
            
        }
        for (int i = 0; i < yourButton.Length; i++)
            {
                Button btn = yourButton[i].GetComponent<Button>();
                Image img = yourButton[i].GetComponent<Image>();
                Text text = yourButton[i].GetComponentInChildren<Text>();
                
                

                yourButton[i].enabled = !(yourButton[i].enabled);
                img.enabled = !(img.enabled);
                text.enabled = !(text.enabled);


            }
        

        

       
    }
    void TaskOnClick(string n)
    {
        if (current.Length >= 4)
        {
            current = n;
        }
        else { current += n; }
        
    }

}
