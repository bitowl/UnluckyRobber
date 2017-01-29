using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public float WaitTime = 2;
    public float _alreadyWaited = 0;

    private string[] _continueKeys =
    {
        "P1 A", "P1 B", "P1 X", "P1 Y",
        "P2 A", "P2 B", "P2 X", "P2 Y"
    };
    
    public void Start()
    {
        _alreadyWaited = 0;
    }

    public void Update()
    {
        _alreadyWaited += Time.deltaTime;
        foreach (var key in _continueKeys)
        {
            if (Input.GetButtonDown(key))
            {
                if (_alreadyWaited >= WaitTime) { 
                    Restart();
                }
            }
        }
    }

    public void Restart()
    {
        GameManager.instance.Restart();
        _alreadyWaited = 0;
    }
}