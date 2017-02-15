using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public SaveData playerData;
    public bool gamePaused = false;

    public GameObject playerPrefab;
    [HideInInspector]
    public GameObject playerReference;

    //Create a singleton to contain all Gamedata
    private static GameManager manager = null;
    public static GameManager Manager
    {
        get { return manager; }
    }

    void Awake()
    {
        if (manager != this && manager != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            manager = this;
            DontDestroyOnLoad(this.gameObject);

            print("Loaded");

            HandleSaveData();
        }
    }

    void HandleSaveData()
    {
        if (!File.Exists(Application.dataPath + "\\Data\\player.dat"))
            playerData.Save();
        else
        {
            playerData = playerData.Load();
            //playerData.Save(); //If the save version needed to be updated, this will keep the changes
        }
    }

    public void PlacePlayer(float xPos, float yPos)
    {
        playerReference.transform.position = new Vector3(xPos, yPos);
    }

    public void SpawnPlayer(float xPos, float yPos)
    {
        playerReference = Instantiate(playerPrefab, new Vector3(xPos, yPos), Quaternion.identity);
    }

    public void ReloadGame()
    {
        playerData = playerData.Load();
        SceneManager.LoadScene(playerData.saveScene);
    }

    public void DecreaseEnergy(float energyConsumption)
    {
        if (!gamePaused)
        {
            playerData.energyReserve -= energyConsumption;
        }
        if (playerData.energyReserve <= 0)
        {
            print("Game Over: things go here");
        }
    }

    public void DrawMap()
    {

    }
}
