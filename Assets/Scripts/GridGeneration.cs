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
    public List<GameObject> ammoPickups = new List<GameObject>();

    public bool skipIntro; 
    
    public List<int> introTrace = new List<int>();

    //List of the types of tiles required
    public GameObject moveTile;
    public GameObject obsticalTile;
    public GameObject goalTile;
    public GameObject destructableTile;
    public GameObject edgePiece;
    public GameObject cornerPiece;

    public GameObject player;
    public GameObject enemy; 
    public GameObject ammoPickup; 

    public int numberOfXGrid;
    public int numberOfYGrid;

    public int startingTile;
    public int startingTileEnemy;
    private TileType[,] tiles; 


    void Start()
    {
        ChooseTileType();
        SetLevelBoarders();
        InitialiseTiles();
        SpawnPlayer();
        SpawnEnemy();
        SpawnAmmo();
        if(!skipIntro)
        IntroCinematic(); 
    }
    
    void Update()
    {
        
    }
    
    //Sets type of tile ready for grid generation 
    void ChooseTileType()
    {
        tiles = new TileType[numberOfXGrid, numberOfYGrid];

        for(int i = 0; i < numberOfYGrid; i++)
            tiles[i, 6] = TileType.obstical;

        for (int i = 0; i < numberOfYGrid; i++)
            tiles[i, 7] = TileType.goal;

        tiles[4, 6] = TileType.destructable;
        tiles[2, 1] = TileType.obstical;
        tiles[2, 2] = TileType.obstical;
        tiles[2, 3] = TileType.obstical;
        tiles[2, 4] = TileType.obstical;
        tiles[3, 4] = TileType.obstical;
        tiles[5, 4] = TileType.obstical;
        tiles[6, 4] = TileType.obstical;
        tiles[7, 4] = TileType.obstical;

    }

    void SetLevelBoarders()
    {
        for(int i = 0; i < numberOfXGrid; i++)
        {
            for(int j = 0; j < numberOfYGrid; j++)
            {
                if ((i == 0 || i == numberOfXGrid - 1) || (j == 0 || j == numberOfYGrid - 1))
                {
                    if(i < numberOfXGrid - 1 && j == 0)
                        tiles[i, j] = TileType.bottomBoundary;
                    else if(i < numberOfXGrid && j == numberOfYGrid - 1)
                        tiles[i, j] = TileType.topBoundary;
                    else if (j < numberOfYGrid - 1 && i == 0)
                        tiles[i, j] = TileType.leftBoundary;
                    else if (j < numberOfYGrid && i == numberOfXGrid - 1)
                        tiles[i, j] = TileType.rightBoundary;

                    if (i == 0 && j == 0)
                        tiles[i, j] = TileType.bottomLeftCorner;
                    if (i == 0 && j == numberOfYGrid - 1)
                        tiles[i, j] = TileType.topLeftCorner;
                    if (i == numberOfXGrid - 1 && j == 0)
                        tiles[i, j] = TileType.bottomRightCorner;
                    if (i == numberOfXGrid - 1 && j == numberOfYGrid - 1)
                        tiles[i, j] = TileType.topRightCorner;
                    
                }
            }
        }
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
                else if (tiles[i, j] == TileType.leftBoundary)
                {
                    grid.type = TileType.leftBoundary;
                    grid.gridSquare = Instantiate(edgePiece, gridPos, Quaternion.Euler(0,0, 90)) as GameObject;
                }
                else if (tiles[i, j] == TileType.rightBoundary)
                {
                    grid.type = TileType.rightBoundary;
                    grid.gridSquare = Instantiate(edgePiece, gridPos, Quaternion.Euler(0, 0, -90)) as GameObject;
                }
                else if (tiles[i, j] == TileType.topBoundary)
                {
                    grid.type = TileType.topBoundary;
                    grid.gridSquare = Instantiate(edgePiece, gridPos, Quaternion.identity) as GameObject;
                }
                else if (tiles[i, j] == TileType.bottomBoundary)
                {
                    grid.type = TileType.bottomBoundary;
                    grid.gridSquare = Instantiate(edgePiece, gridPos, Quaternion.Euler(0, 0, -180)) as GameObject;
                }
                else if(tiles[i, j] == TileType.destructable)
                {
                    grid.type = TileType.destructable;
                    grid.gridSquare = Instantiate(destructableTile, gridPos, Quaternion.identity) as GameObject;
                    grid.gridSquare.GetComponent<TileDestroy>().GetGameManager(this, i, j);
                }
                else if (tiles[i, j] == TileType.bottomLeftCorner)
                {
                    grid.type = TileType.bottomLeftCorner;
                    grid.gridSquare = Instantiate(cornerPiece, gridPos, Quaternion.Euler(0, 0, 90)) as GameObject;
                }
                else if (tiles[i, j] == TileType.bottomRightCorner)
                {
                    grid.type = TileType.bottomRightCorner;
                    grid.gridSquare = Instantiate(cornerPiece, gridPos, Quaternion.Euler(0, 0, 180)) as GameObject;
                }
                else if (tiles[i, j] == TileType.topLeftCorner)
                {
                    grid.type = TileType.topLeftCorner;
                    grid.gridSquare = Instantiate(cornerPiece, gridPos, Quaternion.identity) as GameObject;
                }
                else if (tiles[i, j] == TileType.topRightCorner)
                {
                    grid.type = TileType.topRightCorner;
                    grid.gridSquare = Instantiate(cornerPiece, gridPos, Quaternion.Euler(0, 0, -90)) as GameObject;
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

    public void DestroyTile(int xPos, int yPos)
    {
        GridSquare newSquare;
        for (int i = 0; i < gridSquares.Count ; i++)
        {
            //Finds the correct tile at the right X and Y position and moves the player there
            if ((yPos == gridSquares[i].y && xPos == gridSquares[i].x))
            {
                newSquare.x = xPos;
                newSquare.y = yPos;

                newSquare.type = TileType.moveable;
                newSquare.gridSquare = Instantiate(moveTile, gridSquares[i].gridSquare.transform.position, Quaternion.identity) as GameObject;
                
                Destroy(gridSquares[i].gridSquare);
                gridSquares.RemoveAt(i);

                gridSquares.Insert(i, newSquare);
                break;
            }
        }
    }

    //Spawns in the player 
    void SpawnPlayer()
    {
        GameObject Player = Instantiate(player, gridSquares[startingTile].gridSquare.transform.position, Quaternion.identity) as GameObject;
        gameObject.GetComponent<CodeInput>().GetPlayer(Player); 
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, gridSquares[startingTileEnemy].gridSquare.transform.position, Quaternion.identity);
    }

    void IntroCinematic()
    {
        for (int i = 0; i < gridSquares.Count; i++)
        {
            //Finds the correct tile at the right X and Y position and moves the player there
            if (gridSquares[i].type == TileType.destructable)
            {
                introTrace.Add(i);
            }
        }

        introTrace.Add(47);
        introTrace.Add(startingTile);
        Debug.Log(introTrace[0]);
        var c = player.gameObject.GetComponent<CameraManager>();
        c.StartCinematic(); 
    }

    void SpawnAmmo()
    {
        GameObject pickup = Instantiate(ammoPickup, gridSquares[47].gridSquare.transform.position, Quaternion.identity) as GameObject;
        ammoPickups.Add(pickup);
    }
}

public enum TileType
{
    moveable, 
    obstical,
    leftBoundary,
    rightBoundary,
    topBoundary,
    bottomBoundary,
    topRightCorner,
    bottomRightCorner,
    topLeftCorner,
    bottomLeftCorner,
    goal,
    destructable
}