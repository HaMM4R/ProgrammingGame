using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject cam;
    public GridGeneration g; 
    public bool cinematicOver;

    void Start()
    {
        cinematicOver = false;
        GetCamera();
    }

    void GetCamera()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (cam != null && cinematicOver)
            FollowPlayer();
        else if (cam != null)
            InstructionCinematic(); 
    }

    void FollowPlayer()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }

    public void StartCinematic(GridGeneration grid)
    {
        cinematicOver = false;
        g = grid;
    }

    int i = 0;
    void InstructionCinematic()
    {
        Debug.Log(g.introTrace.Count);
        Debug.Log(i); 
        if (g != null)
        {
            if (i < g.introTrace.Count)
            {
                Transform target = g.gridSquares[g.introTrace[i]].gridSquare.transform;
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, target.position, Time.deltaTime * 2);

                if (Vector2.Distance(transform.position, target.position) < 0.001f)
                {
                    i++;
                }
            }
            else
                cinematicOver = true;
        }
    }
}
