using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountVictim : MonoBehaviour
{
    public Transform MountPoint;
    public Transform GameWorld;

    private Victim _victim;
    private Victim _victimInReach;


    // throw strength
    public Vector3 MinThrowForce;
    public Vector3 MaxThrowForce;
    private bool _beginThrow;
    private float _throwTime;
    public float MaxThrowTime;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
	    if (_beginThrow)
	    {
	        _throwTime += Time.deltaTime;
	    }
        if (Input.GetButtonDown("Fire1"))
        {
            ThrowingButtonDown();

        }
        if (Input.GetButtonUp("Fire1"))
        {
            ThrowingButtonUp();

        }

    }


    void ThrowingButtonDown()
    {
        if (_victim != null)
        {
            _beginThrow = true;
            _throwTime = 0;
        }
        else if (_victimInReach != null)
        {
            PickUpVictim();
        }

   
    }

    void ThrowingButtonUp()
    {
        if (_victim != null && _beginThrow)
        {
            _beginThrow = false;
            var factor = Mathf.Min(_throwTime / MaxThrowTime, 1);
            ThrowVictim(factor * (MaxThrowForce - MinThrowForce) + MinThrowForce);
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

    private void ThrowVictim(Vector3 ThrowForce)
    {
//        _victim.transform.SetParent(gameWorld);
        _victim.Throw(new Vector3((GetComponent<MovementController>().LookingRight ? 1:-1) * ThrowForce.x, ThrowForce.y, ThrowForce.z));
        _victim = null;
    }
}
