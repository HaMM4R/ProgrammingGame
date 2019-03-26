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
        userCommands.Add("FOR");
        userCommands.Add("INCREMENTBY");
        userCommands.Add("<");
        userCommands.Add(">");
        userCommands.Add("<=");
        userCommands.Add(">=");
        userCommands.Add("=");
        userCommands.Add("!=");
        userCommands.Add("ENDFOR");
        
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

        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] == userCommands[0])
            {
                CallMethods("MOVEUP");
            }
            else if (code[i] == userCommands[1])
            {
                CallMethods("MOVEDOWN");
            }
            else if (code[i] == userCommands[2])
            {
                CallMethods("MOVELEFT");
            }
            else if (code[i] == userCommands[3])
            {
                CallMethods("MOVERIGHT");
            }
            else if (code[i] == userCommands[4])
            {
                CallMethods("ROTATE");
            }
            else if (code[i] == userCommands[5])
            {
                CallMethods("FIRE");
            }
            else if(code[i] == userCommands[6] && i + 6 < code.Length)
            {
                For(code, i);
            }
            /*else
            {
                Debug.Log("INVALID COMMAND");
            }*/
        }
    }

    void For(string[] code, int index)
    {
        int forStart = int.Parse(code[index + 1]);
        int forEnd = int.Parse(code[index + 3]);
        int forIncrement = int.Parse(code[index + 5]);

        List<string> commands = new List<string>();

        Debug.Log(userCommands[14]);

        for (int j = index + 6; j < code.Length; j++)
        {
            if (code[j] != userCommands[14])
            {
                commands.Add(code[j]);
            }
            else
                break;
        }

        HandleFor(commands, forStart, forEnd, forIncrement);
    }

    void HandleFor(List<string> methodToCall, int start, int end, int increment)
    {
        for(int i = start; i < end; i = i + increment)
        {
            Debug.Log("FOR1");
            for (int j = 0; j < methodToCall.Count; j++)
            {
                Debug.Log("FOR2");
                CallMethods(methodToCall[j]);
            }
        }
    }

    void CallMethods(string methodToCall)
    {
        if (methodToCall == "MOVEUP")
            pController.PlayerInput(0);
        else if (methodToCall == "MOVEDOWN")
            pController.PlayerInput(1);
        else if (methodToCall == "MOVELEFT")
            pController.PlayerInput(2);
        else if (methodToCall == "MOVERIGHT")
            pController.PlayerInput(3);
        else if (methodToCall == "ROTATE")
            pController.PlayerInput(4);
        else if (methodToCall == "FIRE")
            pShoot.Fire();
    }
}
