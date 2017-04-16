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
    [HideInInspector]
    public float xPos = 0;
    [HideInInspector]
    public float yPos = 0;


    //Create a singleton to contain all Gamedata
    private static GameManager manager = null;
    public static GameManager Manager
    {
        get { return manager; }
    }

    void Awake()
    {
        print("Called");
        SceneManager.sceneLoaded += FadeIn;
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
            //ENABLED IN FINAL VERSION
            //xPos = playerData.saveXPosition;
            //yPos = playerData.saveYPosition;
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
            
        //For alpha REMOVED IN FINAL VERSION
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerReference = GameObject.FindGameObjectWithTag("Player");
        //PlacePlayer(xPos, yPos);

        //For final version, ENABLED IN FINAL VERSION
        //xPos = playerData.saveXPosition;
        //yPos = playerData.saveYPosition;
        //playerData = playerData.Load();
        //SceneManager.LoadScene(playerData.saveScene);
        //playerReference = GameObject.FindGameObjectWithTag("Player");
        //PlacePlayer(xPos, yPos);
    }

    public void DecreaseEnergy(float energyConsumption)
    {
        if (!gamePaused)
        {
            playerData.energyReserve -= energyConsumption;
        }
        if (playerData.energyReserve <= 0)
        {
            ReloadGame();
        }
    }

    public void FadeIn(Scene scene, LoadSceneMode lsm)
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        object[] tmp = { -1, null };
        GetComponent<FadeSystem>().StartCoroutine("PerformFade", tmp);
        //EMABLED IN FINAL VERSION
        //PlacePlayer(xPos, yPos);
    }

    public void FadeOut(string scene)
    {
        object[] tmp = { 1, scene};
        GetComponent<FadeSystem>().StartCoroutine("PerformFade", tmp);
    }

    public void SpriteFade(bool fadeIn, GameObject obj)
    {
        object[] tmp = { obj, fadeIn};
        GetComponent<FadeSystem>().StartCoroutine("PerformSpriteFade",tmp);
    }

    public void DrawMap()
    {

    }
}
