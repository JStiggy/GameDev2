using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MappingSystem : MonoBehaviour {

    public GameObject mapTile;
    public Sprite[] mapTextures;

    private int[,] mapData = new int[8, 8]
    {
        {-1,-1,-1,-1,-1,-1,-1,-1},
        {-1,-1,-1,-1,-1,-1, 0, 2},
        {-1,-1,-1,-1,-1, 0, 6, 8},
        { 0, 2,-1,-1,-1, 3,-1,-1},
        { 6, 8, 0, 2,-1, 3,-1,-1},
        { 0, 2, 6, 8,-1, 6,-1,-1},
        { 6, 8, 0, 1, 2, 0, 2,-1},
        {-1,-1, 0, 2,-1, 6, 8,-1}
    };

    private Image[] uiIcons;

    // Use this for initialization
    void Start () {
        int count = 0;
        uiIcons = new Image[64];
        for(int i = 0; i < 8; ++i)
        {
            for(int j = 0; j < 8; ++j)
            {
                if (mapData[i, j] != -1)
                {
                    GameObject tmp = Instantiate(mapTile, new Vector3(600 + 20 * j, -100 - 20 * i, 0) + transform.position, Quaternion.identity);
                    tmp.transform.SetParent(this.transform, true);
                    uiIcons[count] = tmp.GetComponent<Image>();
                    tmp.GetComponent<Image>().sprite = mapTextures[mapData[i, j]];
                    tmp.SetActive(false);
                }
                else
                {
                    print("s");
                    uiIcons[count] = null;
                }
                ++count;
            }
        }
        

	}

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (GameManager.Manager.playerData.mapData[i,j] == 1 && uiIcons[count] != null && mapData[i,j] != -1)
                {
                    uiIcons[count].sprite = mapTextures[mapData[i,j]];
                    uiIcons[count].gameObject.SetActive(true);
                }
                ++count;
            }
        }
    }
}