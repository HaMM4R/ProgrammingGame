﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GridGeneration grid;

    int curX;
    int curY;

    public List<int> moveTrace = new List<int>();
    int nextGridSquare;

    float fireDelay = 5;

    public GameObject bullet;
    public GameObject fireLocation; 

    void Start()
    {
        var Manager = GameObject.FindGameObjectWithTag("GameController");
        grid = Manager.GetComponent<GridGeneration>();

        curX = grid.gridSquares[grid.startingTileEnemy].x;
        curY = grid.gridSquares[grid.startingTileEnemy].y;

        transform.rotation = Quaternion.Euler(0,0,-90);
        
        moveTrace.Add(grid.startingTileEnemy);
        moveTrace.Add(grid.startingTileEnemy + grid.numberOfXGrid);
        moveTrace.Add(grid.startingTileEnemy - grid.numberOfXGrid);

    }

    // Update is called once per frame
    void Update()
    {
        //SmoothMove();
        Fire(); 
    }

    void SmoothMove()
    {
        if (moveTrace.Count != 0)
        {
            if (nextGridSquare < moveTrace.Count)
            {
                Transform target = grid.gridSquares[moveTrace[nextGridSquare]].gridSquare.transform;
                this.transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 2);

                if (Vector3.Distance(transform.position, target.position) < 0.001f)
                {
                    nextGridSquare++;
                }
            }
        }

        if (nextGridSquare >= moveTrace.Count)
            nextGridSquare = 0;
    }

    void Fire()
    {
        if (fireDelay > 0)
            fireDelay -= Time.deltaTime;
        else
        {
            Instantiate(bullet, fireLocation.transform.position, fireLocation.transform.rotation);
            fireDelay = 5;
        }
    }
}
