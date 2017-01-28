using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UI UI;

    private int _score;
    private float _time;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    
        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    // Initializes the game for each level.
    void InitGame()
    {
        _time = 100;

    }
	
	// Update is called once per frame
	void Update ()
	{
	    _time -= Time.deltaTime;
	    UI.Time = (int)_time;
        UI.Score = _score;

	}


    public void AddScore(int score)
    {
        _score += score;
    }
}
