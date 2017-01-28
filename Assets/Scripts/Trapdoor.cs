using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{

    public Rigidbody Target;
    public bool OpenOnCollideWithPlayer = true;


	// Use this for initialization
	void Start ()
	{
	    Target.isKinematic = true;
	}

    void OnTriggerEnter(Collider other)
    {
//        Debug.Log("ON TRIGGER ENTER" + other);
        if (other.tag == "Player")
        {
            Open();
        }
    }

    public void Open()
    {
        Target.isKinematic = false;
    }
}
