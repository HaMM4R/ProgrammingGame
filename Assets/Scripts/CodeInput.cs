using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeInput : MonoBehaviour
{
    [Serializable]
    public struct Functions
    {
        public string name;
        public List<string> funcCommands; 
    }
    

    PlayerShoot pShoot;
    PlayerController pController;

    List<string> userCommands = new List<string>();
    public List<Functions> functions = new List<Functions>();

    string userCode;

    bool complete;
    bool failed;
    bool submitted;

    bool allCommandsComplete;
    bool allC; 

    public bool instructionComplete { get { return complete; } set { complete = value; } }
    public bool playerFailed { get { return failed; } set { failed = value; } }

    void Start()
    {
        complete = true;
        failed = false;
        allCommandsComplete = false;
        allC = false; 
        userCommands.Add("TANK.MOVEUP()");
        userCommands.Add("TANK.MOVEDOWN()");
        userCommands.Add("TANK.MOVELEFT()");
        userCommands.Add("TANK.MOVERIGHT()");
        userCommands.Add("TANK.ROTATE()");
        userCommands.Add("TANK.FIRE()");
        userCommands.Add("FOR");
        userCommands.Add("INCREMENTBY");
        userCommands.Add("<");
        userCommands.Add(">");
        userCommands.Add("<=");
        userCommands.Add(">=");
        userCommands.Add("=");
        userCommands.Add("!=");
        userCommands.Add("ENDFOR");
        userCommands.Add("FUNCTION");
        userCommands.Add("ENDFUNCTION");
    }

    void Update()
    {
        BufferCommands();
        PlayerFail(); 
    }

    public void GetPlayer(GameObject p)
    {
        pShoot = p.GetComponent<PlayerShoot>();
        pController = p.GetComponent<PlayerController>(); 
    }

    public void GetCode(string c)
    {
        Debug.Log("Recieved");
        userCode = c;
        submitted = true; 
    }

    void PlayerFail()
    {
        if(complete && allCommandsComplete && allC)
            failed = true; 
    }

    void BufferCommands()
    {
        if(submitted)
        {
            Debug.Log("Submitted 2");
            string[] code = SplitCode(userCode);

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = code[i].ToUpper();
            }

            functions.Clear(); 
            StartCoroutine(SubmitCode(code));
            submitted = false; 
        }
    }    

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

    IEnumerator SubmitCode(string[] code)
    {
        Debug.Log("Submitted 3");
        for (int i = 0; i < code.Length; i++)
        {
            yield return new WaitUntil(() => complete == true);
            
            if (code[i] == userCommands[0])
            {
                CallMethods("TANK.MOVEUP()");
            }
            else if (code[i] == userCommands[1])
            {
                CallMethods("TANK.MOVEDOWN()");
            }
            else if (code[i] == userCommands[2])
            {
                CallMethods("TANK.MOVELEFT()");
            }
            else if (code[i] == userCommands[3])
            {
                CallMethods("TANK.MOVERIGHT()");
            }
            else if (code[i] == userCommands[4])
            {
                CallMethods("TANK.ROTATE()");
            }
            else if (code[i] == userCommands[5])
            {
                CallMethods("TANK.FIRE()");
            }
            else if (code[i] == userCommands[6] && i + 6 < code.Length)
            {
                For(code, i);
            }
            else if (code[i] == userCommands[15])
            {
                HandleFunction(code, i);

                for(int j = i; j < code.Length; j++)
                {
                    if(code[j] == userCommands[16])
                    {
                        i = j; 
                        break; 
                    }
                }
            }

            for(int j = 0; j < functions.Count; j++)
            {
                if(code[i] == functions[j].name)
                {
                    StartCoroutine(CallFunction(j));
                }
            }
        }

        if(functions.Count == 0)
            allC = true;
        
        yield return new WaitUntil(() => allC == true);
        allCommandsComplete = true;
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

        StartCoroutine(HandleFor(commands, forStart, forEnd, forIncrement));
    }

    IEnumerator HandleFor(List<string> methodToCall, int start, int end, int increment)
    {
        for(int i = start; i < end; i = i + increment)
        {
            for (int j = 0; j < methodToCall.Count; j++)
            {
                yield return new WaitUntil(() => complete == true);
                CallMethods(methodToCall[j]);
            }
        }
    }

    void HandleFunction(string[] code, int index)
    {
        Functions f;
        List<string> commands = new List<string>();

        f.name = code[index + 1];

        for (int i = index; i < code.Length; i++)
        {
            if (i != (index + 1) && i != index)
            {
                commands.Add(code[i]);
            }
            
            if (code[i] == "ENDFUNCTION")
            {
                break; 
            }
        }

        f.funcCommands = commands; 
        functions.Add(f);
    }

    IEnumerator CallFunction(int j)
    {
        for (int x = 0; x < functions[j].funcCommands.Count; x++)
        {
            yield return new WaitUntil(() => complete == true);
            if (functions[j].funcCommands[x] == userCommands[6])
            {
                For(functions[j].funcCommands.ToArray(), x);
            }
            else
                CallMethods(functions[j].funcCommands[x]);
        }
        
        allC = true;
    }

    void CallMethods(string methodToCall)
    {
        Debug.Log("Submitted 4");
        if (methodToCall == "TANK.MOVEUP()")
            pController.PlayerInput(0);
        else if (methodToCall == "TANK.MOVEDOWN()")
            pController.PlayerInput(1);
        else if (methodToCall == "TANK.MOVELEFT()")
            pController.PlayerInput(2);
        else if (methodToCall == "TANK.MOVERIGHT()")
            pController.PlayerInput(3);
        else if (methodToCall == "TANK.ROTATE()")
            pController.PlayerInput(4);
        else if (methodToCall == "TANK.FIRE()")
            pShoot.Fire();
    }
}
