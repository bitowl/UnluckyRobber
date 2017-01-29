using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Player _inFire;
    private float inFireTime;
    public float TimeUltilDeath = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (_inFire != null)
	    {
	        inFireTime += Time.deltaTime;
	        if (inFireTime >= TimeUltilDeath)
	        {
	            _inFire.Die("You burned to ashes");
	            _inFire = null;
	        }
	    }
	}
    void OnTriggerEnter(Collider other)
    {
        //        Debug.Log("ON TRIGGER ENTER" + other);
        if (other.tag == "Player")
        {
            _inFire = other.GetComponent<Player>();
            _inFire.Burn();
            inFireTime = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //        Debug.Log("ON TRIGGER ENTER" + other);
        if (other.tag == "Player")
        {
            if (_inFire == other.GetComponent<Player>())
            {
                _inFire = null;
            }
            Debug.Log("player " + other + " leaves fire.");
        }
    }
}
