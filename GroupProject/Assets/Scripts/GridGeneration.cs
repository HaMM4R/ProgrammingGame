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

    private List<GridSquare> gridSquares = new List<GridSquare>();
    public GameObject gridObject;

    public int numberOfXGrid;
    public int numberOfYGrid;


    void Start()
    {
        InitialiseSquares();
        GenerateGrid(); 
    }
    
    void Update()
    {
        
    }

    void InitialiseSquares()
    {
        GridSquare grid;
        grid.gridSquare = gridObject;
        for (int i = 0; i < numberOfXGrid; i++)
        {
            for (int j = 0; j < numberOfYGrid; j++)
            {
                grid.x = i;
                grid.y = j;

                gridSquares.Add(grid);
            }
        }
    }

    void GenerateGrid()
    {
        for (int i = 0; i < (numberOfXGrid * numberOfYGrid); i++)
        {
            Vector3 gridPos = new Vector3(gridSquares[i].x, gridSquares[i].y, 0);
            Instantiate(gridSquares[i].gridSquare, gridPos, Quaternion.identity);
        }
    }
}
