using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInput : MonoBehaviour
{
    PlayerShoot pShoot;
    PlayerController pController;

    List<string> userCommands = new List<string>(); 

    string userCode;

    bool complete;
    bool submitted;

    int commandCounter;

    void Start()
    {
        userCommands.Add("MOVEUP");
        userCommands.Add("MOVEDOWN");
        userCommands.Add("MOVELEFT");
        userCommands.Add("MOVERIGHT");
        userCommands.Add("ROTATE");
        userCommands.Add("FIRE");

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
        if (pController != null)
        {
            if (pController.HasControl)
            {
                userCode = GUI.TextArea(new Rect(10, 10, Screen.width / 4, Screen.height - 100), userCode);

                if (GUI.Button(new Rect(10, Screen.height - 80, Screen.width / 4, 60), "Submit"))
                {
                    submitted = true;
                }
            }
        }
    }

    void BufferCommands()
    {
        if(submitted)
        {
            string[] code = SplitCode(userCode);

            for (int i = 0; i < code.Length; i++)
            {
                Debug.Log(code[i]);
                code[i] = code[i].ToUpper();
            }

            SubmitCode(code);

            userCode = "";
            submitted = false; 
        }
    }

    public bool instructionComplete { get { return complete; } set { complete = value; } } 
    

    string[] SplitCode(string code)
    {
        List<string> returnCode = new List<string>();
        string holder = "";
        int i = 0;

        if (code.Contains("\n") || code.Contains(" "))
        {
            foreach (char c in code)
            {
                i++; 
                if (c != '\n' && c != ' ')
                {
                    holder = holder + c;
                    if(i == code.Length)
                        returnCode.Add(holder);

                }
                else 
                {
                    if(holder != string.Empty)
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
        int numOfCalls = 1;
        for (int i = 0; i < code.Length; i++)
        {
            if (i + 1 < code.Length)
            {
                Debug.Log("TESTNG");
                if (!userCommands.Contains(code[i + 1]))
                {
                    numOfCalls = int.Parse(code[i + 1]);
                }
            }

            if (code[i] == userCommands[0])
            {
                for(int j = 0; j < numOfCalls; j++)
                    pController.PlayerInput(0);
            }
            else if (code[i] == userCommands[1])
            {
                for (int j = 0; j < numOfCalls; j++)
                    pController.PlayerInput(1);
            }
            else if (code[i] == userCommands[2])
            {
                for (int j = 0; j < numOfCalls; j++)
                    pController.PlayerInput(2);
            }
            else if (code[i] == userCommands[3])
            {
                for (int j = 0; j < numOfCalls; j++)
                    pController.PlayerInput(3);
            }
            else if (code[i] == userCommands[4])
            {
                for (int j = 0; j < numOfCalls; j++)
                    pController.PlayerInput(4);
            }
            else if (code[i] == userCommands[0])
            {
                for (int j = 0; j < numOfCalls; j++)
                    pShoot.Fire();
            }
            else
            {
                Debug.Log("INVALID COMMAND");
            }
        }
    }
}
