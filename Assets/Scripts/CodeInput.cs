using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInput : MonoBehaviour
{
    PlayerShoot pShoot;
    PlayerController pController;

    string userCode;

    bool complete;
    bool submitted;

    int commandCounter;

    void Start()
    {
        commandCounter = 0; 
    }

    void Update()
    {
        BufferCommands(); 
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
            submitted = true;
        }
    }

    void BufferCommands()
    {
        if(submitted)
        {
            string[] code = SplitCode(userCode);
            SubmitCode(code);

            userCode = "";
            submitted = false; 
        }
    }

    public bool instructionComplete { get { return complete; } set{ complete = value; } } 
    

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
                if (c != '\n')
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

    //SHOULD PROBABLY REMOVE WHITE SPACE FIRST AND THEN PERFORM OPERATIONS
    //PLAY ABOUT WITH SOON 
    void SubmitCode(string[] code)
    {
        string holder = "";
        int numOfCalls = 0; 

        for(int i = 0; i < code.Length; i++)
        {
            if (code[i].Contains("Fire"))
            {
                if(code[i].Length > 4)
                {
                    holder = code[i].Remove(0,5);
                    numOfCalls = int.Parse(holder); 
                }
                else
                    numOfCalls = 1;

                for(int j = 0; j < numOfCalls; j++)
                    pShoot.Fire();
            }

            if (code[i].Contains("MoveUp"))
            {
                holder = code[i].Remove(0,7);
                numOfCalls = int.Parse(holder); 

                for(int j = 0; j < numOfCalls; j++)
                    pController.PlayerInput(0);
            }

            if (code[i].Contains("MoveDown"))
            {
                holder = code[i].Remove(0,9);
                numOfCalls = int.Parse(holder); 

                for(int j = 0; j < numOfCalls; j++)
                    pController.PlayerInput(1);
            }

            if (code[i].Contains("MoveRight"))
            {
                holder = code[i].Remove(0,10);
                numOfCalls = int.Parse(holder); 

                for(int j = 0; j < numOfCalls; j++)
                    pController.PlayerInput(3);
            }

            if (code[i].Contains("MoveLeft"))
            {
                holder = code[i].Remove(0,9);
                numOfCalls = int.Parse(holder); 

                for(int j = 0; j < numOfCalls; j++)
                    pController.PlayerInput(2);
            }

            if(code[i].Contains("Rotate"))
            {
                pController.PlayerInput(4);
            }
        }
    }
}
