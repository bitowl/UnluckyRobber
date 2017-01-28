using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{

    public Rigidbody Hip;
    private bool _throw;
    private Vector3 _throwForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        if (_throw)
        {
            Debug.Log("THROOOOW FORCE " +_throwForce);
            Hip.AddForce(_throwForce);
            _throw = false;
        }
    }

    public void Throw(Vector3 force)
    {
        _throwForce = force;
        _throw = true;
    }

    public void PickUp(Transform followTransform)
    {
    }

}
