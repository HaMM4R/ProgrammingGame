using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GridGeneration grid;
    private CodeInput codeInput; 

    private PlayerShoot pShoot;
    public CameraManager camManager; 

    int xMax;
    int yMax; 
    int currentX;
    int currentY;

    bool hasControl; 
    public bool HasControl { get { return hasControl; } set { hasControl = value; } }

    int nextGridSquare;
    public List<int> moveTrace = new List<int>();
    public List<int> rotateTrace = new List<int>();

    Quaternion stillRotateTarget;
    int curRot; 

    //Gets the grid and sets up player ready for movement
    void Start()
    {
        hasControl = false; 
        var Manager = GameObject.FindGameObjectWithTag("GameController");
        grid = Manager.GetComponent<GridGeneration>();
        codeInput = Manager.GetComponent<CodeInput>();

        camManager = GetComponent<CameraManager>();
        pShoot = GetComponent<PlayerShoot>(); 

        xMax = grid.numberOfXGrid;
        yMax = grid.numberOfYGrid; 

        currentX = grid.gridSquares[grid.startingTile].x;
        currentY = grid.gridSquares[grid.startingTile].y;

        nextGridSquare = 0;
        curRot = 0; 
    }
    
    void Update()
    {
        hasControl = camManager.cinematicOver; 
        if (hasControl)
        {
            SmoothMove();
            SmoothRotate();
        }
    }

    //Takes input from the player (later will be replaced with the user generated code)
    public void PlayerInput(int direction)
    {
        if (direction <= 3)
            PlayerMove(direction);
        else
            PlayerRotate(-1);
    }

    void PlayerRotate(int degrees)
    {
        if (degrees == -1)
            curRot += 90;
        else
            curRot = degrees;
        stillRotateTarget = Quaternion.Euler(0,0, curRot);
    }

    public void SmoothRotate()
    {
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, stillRotateTarget, Time.deltaTime * 500);
    }

    void PlayerMove(int direction)
    {
        int oldX = currentX;
        int oldY = currentY;

        int rotation = 0;
        
        //Sets the x and y for the player to be moved to
        switch (direction)
        {
            case 0:
                if (currentY != yMax - 1)
                {
                    currentY += 1;
                    rotation = 0; 
                }
                break;
            case 1:
                if (currentY != 0)
                {
                    currentY -= 1;
                    rotation = 180; 
                }
                break;
            case 2:
                if (currentX != 0)
                {
                    currentX -= 1;
                    rotation = 90;
                }
                break;
            case 3:
                if (currentX != xMax - 1)
                {
                    currentX += 1;
                    rotation = 270;
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
                    moveTrace.Add(i);
                    rotateTrace.Add(rotation);
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
                Transform target = grid.gridSquares[moveTrace[nextGridSquare]].gridSquare.transform;
                PlayerRotate(rotateTrace[nextGridSquare]);
                this.transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 2);

                //Ammo pickups
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
                    codeInput.instructionComplete = true; 
                }
            }
        }
    } 
}
