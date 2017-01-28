using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountVictim : MonoBehaviour
{
    public Transform MountPoint;
    public Transform GameWorld;
    public Vector3 ThrowForce;

    private Victim _victim;
    private Victim _victimInReach;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {

            if (_victim != null)
            {
                ThrowVictim();
            }
            else if (_victimInReach != null)
            {
                PickUpVictim();
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (_victim == null && other.gameObject.layer == 9 && other.GetComponent<Victim>() != null) // victim
        {
            Debug.Log("victim in reach");
           _victimInReach = other.GetComponent<Victim>();

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == _victimInReach)
        {
            Debug.Log("victim out of reach");
            _victimInReach = null;
        }
    }

    private void PickUpVictim()
    {
        _victim = _victimInReach;
        _victim.PickUp(MountPoint);
        // _victim.transform.position = Vector3.zero;
        //   _victim.transform.SetParent(transform, false);
    }

    private void ThrowVictim()
    {
//        _victim.transform.SetParent(gameWorld);
        _victim.Throw(new Vector3((GetComponent<MovementController>().LookingRight ? 1:-1) * ThrowForce.x, ThrowForce.y, ThrowForce.z));
        _victim = null;
    }
}
