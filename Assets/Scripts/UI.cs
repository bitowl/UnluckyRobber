using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text ScoreText;
    public Text Score2Text;
    public Text TimeText;
    public GameObject GameOver;
    public GameObject YouWon;

    public int Score1
    {
        set { ScoreText.text = "Score: " + value; }
    }

    public int Score2
    {
        set { Score2Text.text = "Score: " + value; }
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
        Score2Text.gameObject.SetActive(GameManager.instance.Coop);
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
