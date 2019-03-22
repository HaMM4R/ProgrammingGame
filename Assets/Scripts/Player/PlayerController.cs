using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Serializable]
    public struct MoveTrace
    {
        public int movePos;
        public float rotation;
    }
    private GridGeneration grid;

    private PlayerShoot pShoot;

    int xMax;
    int yMax; 
    int currentX;
    int currentY;

    int nextGridSquare;
    public List<MoveTrace> moveTrace = new List<MoveTrace>(); 

    //Gets the grid and sets up player ready for movement
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridGeneration>();
        pShoot = GetComponent<PlayerShoot>(); 

        xMax = grid.numberOfXGrid;
        yMax = grid.numberOfYGrid; 

        currentX = grid.gridSquares[grid.startingTile].x;
        currentY = grid.gridSquares[grid.startingTile].y;

        nextGridSquare = 0; 
    }
    
    
    void Update()
    {
        SmoothMove();
    }

    //Takes input from the player (later will be replaced with the user generated code)
    public void PlayerInput(int direction)
    {
        PlayerMove(direction);
    }

    void PlayerRotate(int direction)
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    }

    void PlayerMove(int direction)
    {
        int oldX = currentX;
        int oldY = currentY;
        MoveTrace trace;

        trace.rotation = 0; 

        //Sets the x and y for the player to be moved to
        switch (direction)
        {
            case 0:
                if (currentY != yMax - 1)
                {
                    currentY += 1;
                    trace.rotation = 0;
                }
                break;
            case 1:
                if (currentY != 0)
                {
                    currentY -= 1;
                    trace.rotation = 180;
                }
                break;
            case 2:
                if (currentX != 0)
                {
                    currentX -= 1;
                    trace.rotation = 270;
                }
                break;
            case 3:
                if (currentX != xMax - 1)
                {
                    currentX += 1;
                    trace.rotation = 90;
                }
                break;
        }

        //Loops through the grid positions 
        for (int i = 0; i < grid.gridSquares.Count ; i++)
        {
            //Finds the correct tile at the right X and Y position and moves the player there
            if ((currentY == grid.gridSquares[i].y && currentX == grid.gridSquares[i].x))
            {
                //Makes sure the tile can be moved into and if not resets the player position to prevent bugs.
                if (grid.gridSquares[i].type != TileType.obstical && grid.gridSquares[i].type != TileType.destructable)
                {
                    //nextGridSquare = i;
                    trace.movePos = i;
                    moveTrace.Add(trace);
                   
                }
                else
                {
                    currentX = oldX;
                    currentY = oldY;
                }
                break;
            }
        }
    }

    void SmoothMove()
    {
        if (moveTrace.Count != 0)
        {
            if (nextGridSquare < moveTrace.Count)
            {
                Transform target = grid.gridSquares[moveTrace[nextGridSquare].movePos].gridSquare.transform;
                target.rotation = target.rotation + Quaternion.Euler(0, moveTrace[nextGridSquare].rotation, 0);
                this.transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 2);
                this.transform.rotation = Quaternion.RotateTowards(transform.rotation, , Time.deltaTime * 2);


                for (int i = 0; i < grid.ammoPickups.Count; i++)
                {
                    if (Vector3.Distance(transform.position, grid.ammoPickups[i].transform.position) < 0.001f)
                    {
                        var Ammo = grid.ammoPickups[i].GetComponent<AmmoPickup>();

                        pShoot.Ammo += Ammo.AddAmmo();
                        Ammo.DestroyAmmo();
                        grid.ammoPickups.RemoveAt(i);
                    }
                }

                if (Vector3.Distance(transform.position, target.position) < 0.001f)
                {
                    nextGridSquare++;
                }
            }
        }
    } 
}
