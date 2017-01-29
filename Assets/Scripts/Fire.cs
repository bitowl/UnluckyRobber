using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Player _inFire;
    private Player _inFire2;
    private float inFireTime;
    private float inFireTime2;
    public float TimeUltilDeath = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_inFire2 != null)
        {
            inFireTime2 += Time.deltaTime;
            if (inFireTime2 >= TimeUltilDeath)
            {
                _inFire2.Die("You burned to ashes");
                _inFire2 = null;
            }
        }

        if (_inFire != null)
	    {
	        inFireTime += Time.deltaTime;
	        if (inFireTime >= TimeUltilDeath)
	        {
	            _inFire.Die("You burned to ashes");
	            _inFire = null;
	            if (_inFire2 != null)
	            {
	                _inFire = _inFire2;
	                inFireTime = inFireTime2;
	            }
	        }
	    }

	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_inFire == null)
            {
                _inFire = other.GetComponent<Player>();
                _inFire.Burn();
                inFireTime = 0;
            }
            else
            {
                _inFire2 = other.GetComponent<Player>();
                _inFire2.Burn();
                inFireTime2 = 0;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_inFire == other.GetComponent<Player>())
            {
                _inFire = null;
                if (_inFire2 != null)
                {
                    _inFire = _inFire2;
                    inFireTime = inFireTime2;
                }
            }
            if (_inFire2 == other.GetComponent<Player>())
            {
                _inFire2 = null;
            }
            Debug.Log("player " + other + " leaves fire.");
        }
    }
}
