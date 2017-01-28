using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{

    public MountVictim MountVictim;
    public Transform PunchPoint;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
    
        if (Input.GetButtonDown("Fire1"))
        {
            if (MountVictim.CanThrowSometing)
            {
                MountVictim.ThrowingButtonDown();
            }
            else
            {
                Punch();
            }

        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (MountVictim.CanThrowSometing)
            {
                MountVictim.ThrowingButtonUp();
            }
        }

    }

    public void Punch()
    {
        
    }

    public void Die()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
    }
}
