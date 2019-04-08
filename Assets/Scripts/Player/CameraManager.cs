using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject cam;
    GridGeneration g; 

    void Start()
    {
        GetCamera();
    }

    void GetCamera()
    {
        g = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GridGeneration>();

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Camera mainCam = cam.gameObject.GetComponent<Camera>();

        if (g.level == 0)
            mainCam.orthographicSize = 5;

        if (g.level == 1)
            mainCam.orthographicSize = 9.5f;
        
        if (g.level == 2)
            mainCam.orthographicSize = 5;
    }
}
