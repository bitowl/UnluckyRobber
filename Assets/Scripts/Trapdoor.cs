using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{

    public Rigidbody Target;
    public bool OpenOnCollideWithPlayer = true;
    public bool _opened = false;

	// Use this for initialization
	void Start ()
	{
	    Target.isKinematic = true;
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ON TRIGGER ENTER" + other);
        if (!_opened && other.tag == "Player")
        {
            _opened = true;
            other.GetComponent<SoundPlayer>().Trap();
            Open();
        }
    }

    public void Open()
    {
        Target.isKinematic = false;
    }
}
