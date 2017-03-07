using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MappingSystem : MonoBehaviour {

    public GameObject mapTile;
    public Sprite[] mapTextures;

    private int[,] mapData = new int[5, 5]
    {
        {-1,-1,-1,-1,-1},
        { 0, 2,-1,-1,-1},
        { 6, 8,-1,-1,-1},
        {-1,-1, 7, 8,-1},
        {-1,-1,-1,-1,-1}
    };

    private Image[] uiIcons;

    // Use this for initialization
    void Start () {
        int count = 0;
        uiIcons = new Image[25];
        for(int i = 0; i < 5; ++i)
        {
            for(int j = 0; j < 5; ++j)
            {
                if (mapData[i, j] != -1)
                {
                    GameObject tmp = Instantiate(mapTile, new Vector3(50 + 20 * j, -50 - 20 * i, 0) + transform.position, Quaternion.identity);
                    tmp.transform.SetParent(this.transform, true);
                    uiIcons[count] = tmp.GetComponent<Image>();
                    tmp.SetActive(false);
                }
                else
                {
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
        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                if(GameManager.Manager.playerData.mapData[i,j] == 1 && uiIcons[count] != null && mapData[i,j] != -1)
                {
                    uiIcons[count].sprite = mapTextures[mapData[i,j]];
                    uiIcons[count].gameObject.SetActive(true);
                }
                ++count;
            }
        }
    }
}