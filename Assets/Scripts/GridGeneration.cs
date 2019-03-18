using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{
    [Serializable]
    public struct GridSquare
    {
        public GameObject gridSquare;
        public int x;
        public int y; 
    }

    public List<GridSquare> gridSquares = new List<GridSquare>();
    public GameObject gridObject;
    public GameObject player; 

    public int numberOfXGrid;
    public int numberOfYGrid;

    public int startingTile; 

    void Start()
    {
        InitialiseSquares();
        SpawnPlayer();
    }
    
    void Update()
    {
        
    }

    void InitialiseSquares()
    {
        GridSquare grid;
        for (int i = 0; i < numberOfXGrid; i++)
        {
            for (int j = 0; j < numberOfYGrid; j++)
            {
                grid.x = i;
                grid.y = j;
                Vector3 gridPos = new Vector3(i, j, 0);
                grid.gridSquare = Instantiate(gridObject, gridPos, Quaternion.identity) as GameObject;

                gridSquares.Add(grid);
            }
        }
    }

    void SpawnPlayer()
    {
        Instantiate(player, gridSquares[startingTile].gridSquare.transform.position, Quaternion.identity);
    }
}
