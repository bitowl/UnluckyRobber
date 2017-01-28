using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform Target;
    public float Speed;
    private Vector3 _initial;
    private Vector3 _target;

    private float time;
	// Use this for initialization
	void Start ()
	{
	    _initial = transform.position;
	    _target = Target.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    time += Time.deltaTime;
        Debug.Log((0.5f * Mathf.Sin(time * Speed) + 0.5f));
	    transform.position = (0.5f*Mathf.Sin(time * Speed)+0.5f) * (_target - _initial) + _initial;
	}
}
