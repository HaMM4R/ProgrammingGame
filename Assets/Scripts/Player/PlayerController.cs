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
        int switchCase = direction;
        switch (switchCase)
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

        for (int i = 0; i < grid.gridSquares.Count; i++)
        {
            if ((currentY == grid.gridSquares[i].y && currentX == grid.gridSquares[i].x))
            {
                this.transform.position = grid.gridSquares[i].gridSquare.transform.position;
                break;
            }
        }
    }
        
}
