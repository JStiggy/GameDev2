using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour {

	// Use this for initialization
	public void LoadGame() {
        SaveData playerData = new SaveData();
        if (!File.Exists(Application.dataPath + "\\Data\\player.dat"))
            playerData.Save();
        else
        {
            playerData = playerData.Load();
            //playerData.Save(); //If the save version needed to be updated, this will keep the changes
        }
        SceneManager.LoadScene(playerData.saveScene);
    }
	

}
