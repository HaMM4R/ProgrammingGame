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

    //Gets the grid and sets up player ready for movement
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridGeneration>();

        xMax = grid.numberOfXGrid;
        yMax = grid.numberOfYGrid; 

        currentX = grid.gridSquares[grid.startingTile].x;
        currentY = grid.gridSquares[grid.startingTile].y;
    }
    
    
    void Update()
    {
        PlayerInput(); 
    }

    //Takes input from the player (later will be replaced with the user generated code)
    void PlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            PlayerMove(0);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerMove(1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerMove(2);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerMove(3);
        }
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
                    this.transform.position = grid.gridSquares[i].gridSquare.transform.position;
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
        
}
