using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text ScoreText;
    public Text TimeText;
    public GameObject GameOver;

    public int Score
    {
        set { ScoreText.text = "Score: " + value; }
    }

    public int Time
    {
        set
        {
            TimeText.text = "Time: " + FormatTime(value);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private string FormatTime(int time)
    {
        var str = "";
        if (time > 60)
        {
            var mins = time / 60;
            str += mins + "m ";
            time %= 60;
        }
        return str + time + "s";
    }

}
