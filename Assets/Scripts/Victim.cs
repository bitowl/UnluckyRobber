using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{

    public Rigidbody Hip;
    private Transform _followTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (_followTransform != null)
	    {
	        transform.position = _followTransform.position;
	    }
	}

    public void Throw(Vector3 force)
    {

        _followTransform = null;
        Hip.isKinematic = false;
        Hip.AddForce(force);
    }

    public void PickUp(Transform followTransform)
    {
        _followTransform = followTransform;
        Hip.isKinematic = true;
    }

}
