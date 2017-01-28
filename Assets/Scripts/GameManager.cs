using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UI UI;

    private int score;
    private float time;

	// Use this for initialization
	void Start ()
	{
	    time = 100;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    time -= Time.deltaTime;
	    UI.TimeText.text = "Time: " + time;
	    UI.ScoreText.text = "Score: " + score;
	}
}
