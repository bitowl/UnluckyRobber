using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaded : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    GameManager.instance.StartLevelAfterLoad();

	}
	
}
