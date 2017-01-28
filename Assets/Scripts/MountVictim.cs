using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountVictim : MonoBehaviour
{
    public MovementController MovementController;

    public HingeJoint MountJoint;
    public Transform MountPoint;

    private Victim _victim;
    private Victim _victimInReach;


    // throw strength
    public Vector3 MinThrowForce;
    public Vector3 MaxThrowForce;
    private bool _beginThrow;
    private float _throwTime;
    public float MaxThrowTime;

    public bool CanThrowSometing
    {
        get { return _victim != null || _victimInReach != null; }
    }

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
    }


    public void ThrowingButtonDown()
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

    public void ThrowingButtonUp()
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
            Debug.Log("victim in reach " + other);
           _victimInReach = other.GetComponent<Victim>();

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_victimInReach != null && other.gameObject == _victimInReach.gameObject)
        {
            Debug.Log("victim out of reach");
            _victimInReach = null;
        }
    }

    private void PickUpVictim()
    {
    

        Debug.Log("pick up victim");
        _victim = _victimInReach;
        _victim.transform.position = MountPoint.transform.position;
        MountJoint.connectedBody = _victim.Hip;
        _victim.PickUp(MountPoint);
        // _victim.transform.position = Vector3.zero;
        //   _victim.transform.SetParent(transform, false);
    }

    private void ThrowVictim(Vector3 ThrowForce)
    {
//        _victim.transform.SetParent(gameWorld);
        _victim.Throw(new Vector3((MovementController.LookingRight ? 1:-1) * ThrowForce.x, ThrowForce.y, ThrowForce.z));
        MountJoint.connectedBody = null;
        _victim = null;
    }
}
