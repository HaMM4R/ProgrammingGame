using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject cam;

    void Start()
    {
        GetCamera(); 
    }

    void GetCamera()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cam != null)
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }

}
