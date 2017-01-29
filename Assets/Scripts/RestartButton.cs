using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    private string[] _continueKeys =
    {
        "P1 A", "P1 B", "P1 X", "P1 Y",
        "P2 A", "P2 B", "P2 X", "P2 Y" 
    };
    public void Update()
    {
        foreach (var key in _continueKeys)
        {
            if (Input.GetButtonDown(key))
            {
                Restart();
            }
        }
    }
    public void Restart()
    {
        GameManager.instance.Restart();
    }
}