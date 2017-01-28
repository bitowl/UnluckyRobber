using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountVictim : MonoBehaviour
{
    public Transform gameWorld;

    private Transform _victim;
    private Transform _victimInReach;

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
            _victimInReach = other.transform;

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
        _victim.transform.position = Vector3.zero;
        _victim.SetParent(transform, false);
    }

    private void ThrowVictim()
    {
        _victim.SetParent(gameWorld);
        _victim = null;
    }
}
