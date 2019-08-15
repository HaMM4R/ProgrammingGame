using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionHolder : MonoBehaviour
{
    private int _id; 
    private string _code;

    public int ID { get { return _id; } }
    public string Code { get { return _code; } }

    public void SetValues(int id, string code)
    {
        _id = id;
        _code = code; 
    }

    public void CallMethod()
    {
        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            var manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<CodeInput>();
            manager.SetCode(_code);
        }
    }
}
