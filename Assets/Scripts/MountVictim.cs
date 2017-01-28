using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountVictim : MonoBehaviour
{

    public Transform MountPoint;
    public Transform Victim;

	// Use this for initialization
	void Start ()
	{
	    Victim.transform.position = Vector3.zero;
		Victim.SetParent(MountPoint, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
