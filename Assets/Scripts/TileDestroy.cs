using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDestroy : MonoBehaviour
{
    GridGeneration grid; 
    int xPos;
    int yPos;

    public void GetGameManager(GridGeneration g, int x, int y)
    {
        grid = g; 
        xPos = x; 
        yPos = y; 
    }

    public void Destroy()
    {
        grid.DestroyTile(xPos, yPos);
    }
}
