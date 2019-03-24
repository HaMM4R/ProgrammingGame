using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject cam;
    public GridGeneration g; 
    public bool cinematicOver;
    float cinematicHolder; 
    float cinematicTimer = 0;

    float cameraZoom;
    bool displayInstructions;
    GUIStyle style;

    void Start()
    {
        cinematicOver = false;
        displayInstructions = false; 
        cinematicHolder = 4;
        cinematicTimer = cinematicHolder;
        style = GUIStyle.none;
        style.wordWrap = true;
        GetCamera();
    }

    void GetCamera()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(g == null)
            g = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GridGeneration>();

        if (cam != null && g != null)
        {
            FollowPlayer();
            InstructionCinematic();
        }
    }

    void FollowPlayer()
    {
        if(cinematicOver)
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }

    public void StartCinematic()
    {
        cinematicOver = false;
    }

    int i = 0;
    void InstructionCinematic()
    {
        Camera mainCam = cam.gameObject.GetComponent<Camera>();
        if (!cinematicOver)
        { 
            if(mainCam.orthographicSize > 3)
                mainCam.orthographicSize -= Time.deltaTime * 6;

            if (g != null)
            {
                if (i < g.introTrace.Count)
                {
                    Transform target = g.gridSquares[g.introTrace[i]].gridSquare.transform;
                    Vector3 targetPos = new Vector3(target.position.x, target.position.y, cam.transform.position.z);
                    cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetPos, Time.deltaTime * 4);

                    if (Vector3.Distance(cam.transform.position, targetPos) < 0.001f)
                    {
                        cinematicTimer -= Time.deltaTime;
                        displayInstructions = true;
                        if (cinematicTimer <= 0)
                        {
                            displayInstructions = false; 
                            cinematicTimer = cinematicHolder;
                            i++;
                        }
                    }
                }
                else
                    cinematicOver = true;
            }
        }
        
        if (mainCam.orthographicSize < 5 && cinematicOver)
            mainCam.orthographicSize += Time.deltaTime * 6;
    }

    private void OnGUI()
    {
        string instructions = "";
        if (displayInstructions)
        {
            if (i == 0)
                instructions = "These blocks are destroyable, shoot at them to be able to move over them!";
            else if(i == 1)
                instructions = "Navigate over these blocks to pickup ammo!";
            else if(i == 2)
                instructions = "and finally this is you, enter commands in the box to the left to nagivate!";


            GUI.Box(new Rect(Screen.width / 2 - 230, Screen.height / 2 - 100, 200, 200), instructions, style);
        }
    }
}
