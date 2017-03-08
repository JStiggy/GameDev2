using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class DialougeSystem : MonoBehaviour {

    public DialougeData dialougeData;
    private Text displayText;
    private float waitTime = .05f;

	// Use this for initialization
	void Awake () {
        XmlSerializer serializer = new XmlSerializer(typeof(DialougeData));
        FileStream stream = new FileStream(Application.dataPath + "\\Data\\DialougeData.xml", FileMode.Open);
        dialougeData = serializer.Deserialize(stream) as DialougeData;
        stream.Close();

        displayText = GetComponentInChildren<Text>();
    }

    IEnumerator PrintDialouge(int dialougeIndex)
    {
        Dialouge dialouge = dialougeData.Dialouges[dialougeIndex];
        for (int i = 0; i < dialouge.text.Length; i++)
        {
            for(int j = 0; j < dialouge.text[i].Length; ++j)
            {
                displayText.text += dialouge.text[i][j];
                yield return new WaitForSeconds(waitTime);
            }
            if(dialouge.autoScroll != -1)
            {
                yield return new WaitForSeconds(dialouge.autoScroll);
                displayText.text = "";
            }
            else
            {
                while(!Input.GetKeyDown(KeyCode.Space))
                {
                    yield return null;
                }
                displayText.text = "";
            }
        }

    }

}
