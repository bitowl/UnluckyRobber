using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterTime : MonoBehaviour
{

    public float TimeLeft = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    TimeLeft -= Time.deltaTime;
	    if (TimeLeft <= 0)
	    {
	        Destroy(gameObject);
	    }
	}
}
