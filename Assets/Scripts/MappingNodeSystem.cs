using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingNodeSystem : MonoBehaviour {

    public Vector3 nodeTopLeft;
    public Vector3 nodeBotRight;

    //Number of tiles in the map the area represents
    public int tileWidth;
    public int tileHeight;

    //Position on Map of upper right corner
    public int xPosition;
    public int yPosition;

    public Vector2 tileDimensions;

    void Start()
    {
        nodeTopLeft = transform.GetChild(0).position;
        nodeBotRight = transform.GetChild(1).position;

        float sectorWidth = Mathf.Abs((nodeTopLeft.x - nodeBotRight.x) / tileWidth);
        float sectorHeight = -Mathf.Abs((nodeTopLeft.y - nodeBotRight.y) / tileHeight);

        tileDimensions = new Vector2(sectorWidth, sectorHeight);
    }
	
    //Iterate through all sections of the map
    void Update()
    {
        for (int x = 0; x < tileWidth; x++)
        {
            for (int y = 0; y < tileHeight; y++)
            {
                if (PlayerInSector(new Vector2(nodeTopLeft.x + x * tileDimensions.x, nodeTopLeft.y + y * tileDimensions.y)))
                {
                    GameManager.Manager.playerData.mapData[yPosition + y, xPosition + x] = 1;
                }
            }
        }
    }

    bool PlayerInSector(Vector2 sectorLocation)
    {
        //print(sectorLocation + " " + (sectorLocation + tileDimensions));
        if (Physics2D.OverlapArea(sectorLocation, sectorLocation + tileDimensions, 1 << 8) != null)
            return true;
        else
            return false;
    }
}
