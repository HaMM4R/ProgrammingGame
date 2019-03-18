using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GridGeneration grid;
    int xStep;
    int yStep; 
    int currentTile; 

    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridGeneration>();
        xStep = grid.numberOfYGrid;
        yStep = grid.numberOfXGrid; 
        currentTile = grid.startingTile; 
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
                currentTile += 1;
                if ((currentTile > grid.gridSquares.Count - 1 || currentTile < 0) || currentTile == yStep)
                    currentTile -= 1; 
                this.transform.position = grid.gridSquares[currentTile].gridSquare.transform.position; 
                break;
            case 1:
                currentTile -= 1;
                if (currentTile > grid.gridSquares.Count - 1 || currentTile < 0)
                    currentTile += 1;
                this.transform.position = grid.gridSquares[currentTile].gridSquare.transform.position;
                break;
            case 2:
                currentTile = currentTile - xStep;
                if (currentTile > grid.gridSquares.Count - 1 || currentTile < 0)
                    currentTile = currentTile + xStep;
                this.transform.position = grid.gridSquares[currentTile].gridSquare.transform.position;
                break;
            case 3:
                currentTile = currentTile + xStep;
                if (currentTile > grid.gridSquares.Count - 1 || currentTile < 0)
                    currentTile = currentTile - xStep;
                this.transform.position = grid.gridSquares[currentTile].gridSquare.transform.position;
                break;
        }     
    }
}
