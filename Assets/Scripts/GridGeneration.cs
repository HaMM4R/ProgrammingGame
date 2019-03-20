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
        public TileType type; 
    }

    public List<GridSquare> gridSquares = new List<GridSquare>();

    //List of the types of tiles required
    public GameObject moveTile;
    public GameObject obsticalTile;
    public GameObject goalTile;

    public GameObject player; 

    public int numberOfXGrid;
    public int numberOfYGrid;

    public int startingTile;
    private TileType[,] tiles; 

    void Start()
    {
        ChooseTileType(); 
        InitialiseTiles();
        SpawnPlayer();
    }
    
    void Update()
    {
        
    }

    //Sets type of tile ready for grid generation 
    void ChooseTileType()
    {
        tiles = new TileType[numberOfXGrid, numberOfYGrid];
        tiles[1, 0] = TileType.obstical;
        tiles[1, 1] = TileType.obstical;
        tiles[1, 2] = TileType.obstical;
        tiles[1, 3] = TileType.obstical;
        tiles[2, 3] = TileType.obstical;
        tiles[3, 3] = TileType.obstical;
        tiles[3, 2] = TileType.obstical;
        tiles[3, 1] = TileType.obstical;
        tiles[2, 2] = TileType.goal;

    }

    //Spawns in level
    void InitialiseTiles()
    {
        GridSquare grid;
        for (int i = 0; i < numberOfXGrid; i++)
        {
            for (int j = 0; j < numberOfYGrid; j++)
            {
                grid.x = i;
                grid.y = j;
                
                //Calculates positions to instantiate tiles at
                Vector3 gridPos = new Vector3(i, j, 0);
                Vector3 centerVector = new Vector3(-(numberOfXGrid / 2), -(numberOfYGrid / 2), 0);
                gridPos = gridPos + centerVector; 

                //Instantiates the correct kind of tile and sets type to be checked by player
                if (tiles[i, j] == TileType.moveable)
                {
                    grid.type = TileType.moveable;
                    grid.gridSquare = Instantiate(moveTile, gridPos, Quaternion.identity) as GameObject;
                }
                else if (tiles[i, j] == TileType.obstical)
                {
                    grid.type = TileType.obstical;
                    grid.gridSquare = Instantiate(obsticalTile, gridPos, Quaternion.identity) as GameObject;
                }
                else
                {
                    grid.type = TileType.goal;
                    grid.gridSquare = Instantiate(goalTile, gridPos, Quaternion.identity) as GameObject;
                }

                //Adds the grid square to a list of grid squares for movement
                gridSquares.Add(grid);
            }
        }
    }

    //Spawns in the player 
    void SpawnPlayer()
    {
        GameObject Player = Instantiate(player, gridSquares[startingTile].gridSquare.transform.position, Quaternion.identity) as GameObject;
        gameObject.GetComponent<CodeInput>().GetPlayer(Player); 
    }
}

public enum TileType
{
    moveable, 
    obstical,
    goal
}