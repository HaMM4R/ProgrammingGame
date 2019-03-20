using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GridGeneration grid; 

    int xMax;
    int yMax; 
    int currentX;
    int currentY;

    int nextGridSquare;
    public List<int> moveTrace = new List<int>(); 

    //Gets the grid and sets up player ready for movement
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridGeneration>();

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
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerMove(direction);
            PlayerRotate(0);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerMove(1);
            PlayerRotate(180);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerMove(2);
            PlayerRotate(90);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerMove(3);
            PlayerRotate(270);
        }*/
    }

    void PlayerRotate(int direction)
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    }

    void PlayerMove(int direction)
    {
        int oldX = currentX;
        int oldY = currentY; 

        //Sets the x and y for the player to be moved to
        switch (direction)
        {
            case 0:
                if(currentY != yMax - 1)
                    currentY += 1; 
                break;
            case 1:
                if (currentY != 0)
                    currentY -= 1;
                break;
            case 2:
                if (currentX != 0)
                    currentX -= 1;
                break;
            case 3:

                if (currentX != xMax - 1)
                    currentX += 1;
                break;
        }

        //Loops through the grid positions 
        for (int i = 0; i < grid.gridSquares.Count ; i++)
        {
            //Finds the correct tile at the right X and Y position and moves the player there
            if ((currentY == grid.gridSquares[i].y && currentX == grid.gridSquares[i].x))
            {
                //Makes sure the tile can be moved into and if not resets the player position to prevent bugs.
                if (grid.gridSquares[i].type != TileType.obstical)
                {
                    //nextGridSquare = i;
                    moveTrace.Add(i);
                   
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
                Vector3 target = grid.gridSquares[moveTrace[nextGridSquare]].gridSquare.transform.position;
                this.transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 2);

                if (Vector3.Distance(transform.position, target) < 0.001f)
                {
                    nextGridSquare++;
                }
            }
        }
    } 
}
