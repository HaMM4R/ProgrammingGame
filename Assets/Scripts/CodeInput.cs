using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInput : MonoBehaviour
{
    PlayerShoot pShoot;
    PlayerController pController;

    string userCode; 

    void Start()
    {
        
    }
    
    public void GetPlayer(GameObject p)
    {
        pShoot = p.GetComponent<PlayerShoot>();
        pController = p.GetComponent<PlayerController>(); 
    }

    private void OnGUI()
    {
        userCode = GUI.TextArea(new Rect(10, 10, Screen.width / 4, Screen.height - 100), userCode);

        if(GUI.Button(new Rect(10, Screen.height - 80, Screen.width / 4, 60), "Submit"))
        {
            string[] code = SplitCode(userCode);
            SubmitCode(code);
            
            userCode = ""; 
        }
    }

    string[] SplitCode(string code)
    {
        List<string> returnCode = new List<string>();
        string holder = "";
        int i = 0;

        if (code.Contains("\n"))
        {
            foreach (char c in code)
            {
                i++; 
                if (c != '\n')//Check to see if of char is the same as the length of the string
                {
                    holder = holder + c;
                    if(i == code.Length)
                        returnCode.Add(holder);

                }
                else 
                {
                    returnCode.Add(holder);
                    holder = "";
                }
            }
        }
        else
            returnCode.Add(code);
        
        return returnCode.ToArray();
    }

    void SubmitCode(string[] code)
    {
        for(int i = 0; i < code.Length; i++)
        {
            if (code[i] == "Fire")
            {
                pShoot.Fire();
            }

            if(code[i] == "MoveUp")
            {
                pController.PlayerInput(0);
            }
            if (code[i] == "MoveDown")
            {
                pController.PlayerInput(1);
            }
            if (code[i] == "MoveRight")
            {
                pController.PlayerInput(3);
            }
            if (code[i] == "MoveLeft")
            {
                pController.PlayerInput(2);
            }
        }
    }
}
