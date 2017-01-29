using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{

    public Image OnePlayer;
    public Image TwoPlayer;

    public float _player1;
    public float _player2;
    public bool _twoPlayers = false;


    private string[] _startKeys =
    {
        "P1 A", "P1 B", "P1 X", "P1 Y",
        "P2 A", "P2 B", "P2 X", "P2 Y"
    };
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (ShouldToggle(_player1, Input.GetAxis("P1 Horizontal")))
	    {
	        Toggle();
	    }
	    _player1 = Input.GetAxis("P1 Horizontal");

        if (ShouldToggle(_player2, Input.GetAxis("P2 Horizontal")))
        {
            Toggle();
        }
	    _player2 = Input.GetAxis("P2 Horizontal");

        foreach (var key in _startKeys)
        {
            if (Input.GetButtonDown(key))
            {
                StartGame();
            }
        }
    }

    void Toggle()
    {
        _twoPlayers = !_twoPlayers;
        OnePlayer.color = new Color(1, 1, 1, _twoPlayers ? 0.5f: 1);
        TwoPlayer.color = new Color(1, 1, 1, _twoPlayers ? 1 : 0.5f);

    }

    void StartGame()
    {
        if (_twoPlayers)
        {
            SceneManager.LoadScene("GameCoop");
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }

    bool ShouldToggle(float a, float b)
    {
        if (a < 0 && b > 0)
        {
            return true;
        }
        if (a > 0 && b < 0)
        {
            return true;
        }

        if (a == 0 && b > 0)
        {
            return true;
        }
        if (a == 0 && b < 0)
        {
            return true;
        }

        return false;
    }

    bool GreaterZero(float f)
    {
        return f > 0;
    }
}
