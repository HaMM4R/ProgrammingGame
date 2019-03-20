using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInput : MonoBehaviour
{
    GameObject player;

    PlayerShoot pShoot;
    PlayerController pController;


    string userCode; 

    void Start()
    {
        
    }
    
    public void GetPlayer(GameObject p)
    {
        player = p;
        pShoot = p.GetComponent<PlayerShoot>();
        pController = p.GetComponent<PlayerController>(); 
    }

    private void OnGUI()
    {
        userCode = GUI.TextArea(new Rect(10, 10, Screen.width / 4, Screen.height - 100), userCode);

        if(GUI.Button(new Rect(10, Screen.height - 80, Screen.width / 4, 60), "Submit"))
        {
            SubmitCode(userCode);
            userCode = ""; 
        }
    }

    void SubmitCode(string code)
    {
        if (code == "Fire")
        {
            pShoot.Fire();
        }

        if(code == "MoveUp")
        {
            pController.PlayerInput(0);
        }
        if (code == "MoveDown")
        {
            pController.PlayerInput(1);
        }
        if (code == "MoveRight")
        {
            pController.PlayerInput(2);
        }
        if (code == "MoveLeft")
        {
            pController.PlayerInput(3);
        }
    }
}
